import React, { useEffect } from "react";
import {
	Table,
	TableHeader,
	TableColumn,
	TableBody,
	TableRow,
	TableCell,
	Input,
	Button,
	DropdownTrigger,
	Dropdown,
	DropdownMenu,
	DropdownItem,
	Chip,
	Pagination,
	Selection,
	SortDescriptor,
	User,
} from "@nextui-org/react";
import {
	ChevronDownIcon,
	EllipsisVerticalIcon,
	MagnifyingGlassIcon,
	PlusIcon,
} from "@heroicons/react/16/solid";

import { Project } from "../../helpers/Types";
import { projectStatusColorMap } from "../../helpers/Constants";
import { Link, useNavigate } from "react-router-dom";
import UpdateProject from "../../components/projects/UpdateProject";

function capitalize(str: string) {
	return str.charAt(0).toUpperCase() + str.slice(1);
}

const columns = [
	{ name: "ID", uid: "id", sortable: true },
	{ name: "TITLE", uid: "title", sortable: true },
	{ name: "DESCRIPTION", uid: "description" },
	{ name: "STATUS", uid: "status", sortable: true },
	{ name: "CREATED DATE", uid: "createdDate", sortable: true },
	{ name: "ACTIONS", uid: "actions" },
];

const statusOptions = [
	{ name: "Active", uid: "active" },
	{ name: "Inactive", uid: "inactive" },
	{ name: "Suspended", uid: "suspended" },
];

// const INITIAL_VISIBLE_COLUMNS = ["name", "role", "status", "actions"];
const INITIAL_VISIBLE_COLUMNS = ["title", "createdDate", "status", "actions"];

// type Project = (typeof projects)[0];

interface Props {
	authToken: string;
}

export default function ProjectsPage({ authToken }: Readonly<Props>) {
	const navigate = useNavigate();

	const [projects, setProjects] = React.useState<Project[]>([]);
	const fetchProjects = React.useCallback(() => {
		fetch("/api/v1/projects/user", {
			method: "GET",
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${authToken}`,
			},
		})
			.then(async (response) => {
				if (response.ok) {
					const json = await response.json();
					setProjects(() => json.data);
				}
			})
			.catch((error) => {
				console.error(error);
			});
	}, [authToken]);

	useEffect(() => {
		fetchProjects();
	}, [fetchProjects]);

	const [filterValue, setFilterValue] = React.useState("");
	const [selectedKeys, setSelectedKeys] = React.useState<Selection>(
		new Set([])
	);
	const [visibleColumns, setVisibleColumns] = React.useState<Selection>(
		new Set(INITIAL_VISIBLE_COLUMNS)
	);
	const [statusFilter, setStatusFilter] = React.useState<Selection>("all");
	const [rowsPerPage, setRowsPerPage] = React.useState(5);
	const [sortDescriptor, setSortDescriptor] = React.useState<SortDescriptor>({
		column: "createdDate",
		direction: "ascending",
	});

	const [page, setPage] = React.useState(1);

	const hasSearchFilter = Boolean(filterValue);

	const headerColumns = React.useMemo(() => {
		if (visibleColumns === "all") return columns;

		return columns.filter((column) =>
			Array.from(visibleColumns).includes(column.uid)
		);
	}, [visibleColumns]);

	const filteredItems = React.useMemo(() => {
		let filteredProjects = [...projects];

		if (hasSearchFilter) {
			filteredProjects = filteredProjects.filter((projects) =>
				projects.title.toLowerCase().includes(filterValue.toLowerCase())
			);
		}
		if (
			statusFilter !== "all" &&
			Array.from(statusFilter).length !== statusOptions.length
		) {
			filteredProjects = filteredProjects.filter((projects) =>
				Array.from(statusFilter).includes(projects.status)
			);
		}

		return filteredProjects;
	}, [projects, hasSearchFilter, statusFilter, filterValue]);

	const pages = Math.ceil(filteredItems.length / rowsPerPage);

	const items = React.useMemo(() => {
		const start = (page - 1) * rowsPerPage;
		const end = start + rowsPerPage;

		return filteredItems.slice(start, end);
	}, [page, filteredItems, rowsPerPage]);

	const sortedItems = React.useMemo(() => {
		return [...items].sort((a: Project, b: Project) => {
			const first = a[sortDescriptor.column as keyof Project] as number;
			const second = b[sortDescriptor.column as keyof Project] as number;
			const cmp = first < second ? -1 : first > second ? 1 : 0;

			return sortDescriptor.direction === "descending" ? -cmp : cmp;
		});
	}, [sortDescriptor, items]);

	const renderCell = React.useCallback(
		(project: Project, columnKey: React.Key) => {
			const cellValue = project[columnKey as keyof Project];

			switch (columnKey) {
				case "title":
					return (
						<User
							avatarProps={{ radius: "lg", src: project.pictureUrl }}
							description={project.description}
							name={cellValue}
						/>
					);
				case "createdDate":
					return (
						<div className='flex flex-col'>
							<p className='text-bold text-small capitalize'>{cellValue}</p>
							{/* <p className='text-bold text-tiny capitalize text-default-400'>
								{project.createdDate}
							</p> */}
						</div>
					);
				case "status":
					return (
						<Chip
							className='capitalize'
							color={projectStatusColorMap[project.status]}
							size='sm'
							variant='flat'
						>
							{cellValue}
						</Chip>
					);
				case "actions":
					return (
						<div className='relative flex justify-center  items-center gap-2'>
							<Dropdown>
								<DropdownTrigger>
									<Button
										isIconOnly
										size='sm'
										variant='light'
									>
										<EllipsisVerticalIcon className='size-6' />
									</Button>
								</DropdownTrigger>
								<DropdownMenu>
									<DropdownItem
										onPress={() => {
											navigate("/projects/" + project?.id);
										}}
									>
										View
									</DropdownItem>
									<UpdateProject
										project={project}
										authToken={authToken}
										fetchProjects={fetchProjects}
									/>
									<DropdownItem>Delete</DropdownItem>
								</DropdownMenu>
							</Dropdown>
						</div>
					);
				default:
					return cellValue;
			}
		},
		[]
	);

	const onNextPage = React.useCallback(() => {
		if (page < pages) {
			setPage(page + 1);
		}
	}, [page, pages]);

	const onPreviousPage = React.useCallback(() => {
		if (page > 1) {
			setPage(page - 1);
		}
	}, [page]);

	const onRowsPerPageChange = React.useCallback(
		(e: React.ChangeEvent<HTMLSelectElement>) => {
			setRowsPerPage(Number(e.target.value));
			setPage(1);
		},
		[]
	);

	const onSearchChange = React.useCallback((value?: string) => {
		if (value) {
			setFilterValue(value);
			setPage(1);
		} else {
			setFilterValue("");
		}
	}, []);

	const onClear = React.useCallback(() => {
		setFilterValue("");
		setPage(1);
	}, []);

	const topContent = React.useMemo(() => {
		return (
			<div className='flex flex-col gap-4'>
				<div className='flex justify-between gap-3 items-end'>
					<Input
						isClearable
						className='w-full sm:max-w-[44%]'
						placeholder='Search by name...'
						startContent={<MagnifyingGlassIcon className='size-6' />}
						value={filterValue}
						onClear={() => onClear()}
						onValueChange={onSearchChange}
					/>
					<div className='flex gap-3'>
						<Dropdown>
							<DropdownTrigger className='hidden sm:flex'>
								<Button
									endContent={<ChevronDownIcon className='size-6' />}
									variant='flat'
								>
									Status
								</Button>
							</DropdownTrigger>
							<DropdownMenu
								disallowEmptySelection
								aria-label='Table Columns'
								closeOnSelect={false}
								selectedKeys={statusFilter}
								selectionMode='multiple'
								onSelectionChange={setStatusFilter}
							>
								{statusOptions.map((status) => (
									<DropdownItem
										key={status.uid}
										className='capitalize'
									>
										{capitalize(status.name)}
									</DropdownItem>
								))}
							</DropdownMenu>
						</Dropdown>
						<Dropdown>
							<DropdownTrigger className='hidden sm:flex'>
								<Button
									endContent={<ChevronDownIcon className='size-6' />}
									variant='flat'
								>
									Columns
								</Button>
							</DropdownTrigger>
							<DropdownMenu
								disallowEmptySelection
								aria-label='Table Columns'
								closeOnSelect={false}
								selectedKeys={visibleColumns}
								selectionMode='multiple'
								onSelectionChange={setVisibleColumns}
							>
								{columns.map((column) => (
									<DropdownItem
										key={column.uid}
										className='capitalize'
									>
										{capitalize(column.name)}
									</DropdownItem>
								))}
							</DropdownMenu>
						</Dropdown>
						<Button
							color='primary'
							endContent={<PlusIcon className='size-6' />}
						>
							Add New
						</Button>
					</div>
				</div>
				<div className='flex justify-between items-center'>
					<span className='text-default-400 text-small'>
						Total {projects.length} projects
					</span>
					<label className='flex items-center text-default-400 text-small'>
						{"Rows per page: "}
						<select
							className='bg-transparent outline-none text-default-400 text-small'
							onChange={onRowsPerPageChange}
						>
							<option value='5'> 5</option>
							<option value='10'> 10</option>
							<option value='15'> 15</option>
						</select>
					</label>
				</div>
			</div>
		);
	}, [
		filterValue,
		onSearchChange,
		statusFilter,
		visibleColumns,
		projects.length,
		onRowsPerPageChange,
		onClear,
	]);

	const bottomContent = React.useMemo(() => {
		return (
			<div className='py-2 px-2 flex justify-between items-center'>
				<span className='w-[30%] text-small text-default-400'>
					{selectedKeys === "all"
						? "All items selected"
						: `${selectedKeys.size} of ${filteredItems.length} selected`}
				</span>
				<Pagination
					isCompact
					showControls
					showShadow
					color='primary'
					page={page}
					total={pages}
					onChange={setPage}
				/>
				<div className='hidden sm:flex w-[30%] justify-end gap-2'>
					<Button
						isDisabled={pages === 1}
						size='sm'
						variant='flat'
						onPress={onPreviousPage}
					>
						Previous
					</Button>
					<Button
						isDisabled={pages === 1}
						size='sm'
						variant='flat'
						onPress={onNextPage}
					>
						Next
					</Button>
				</div>
			</div>
		);
	}, [
		selectedKeys,
		filteredItems.length,
		page,
		pages,
		onPreviousPage,
		onNextPage,
	]);

	return (
		<Table
			aria-label='Example table with custom cells, pagination and sorting'
			isHeaderSticky
			bottomContent={bottomContent}
			bottomContentPlacement='outside'
			classNames={{
				wrapper: "max-h-[382px]",
			}}
			selectedKeys={selectedKeys}
			selectionMode='single'
			sortDescriptor={sortDescriptor}
			topContent={topContent}
			topContentPlacement='outside'
			onSelectionChange={setSelectedKeys}
			onSortChange={setSortDescriptor}
		>
			<TableHeader columns={headerColumns}>
				{(column) => (
					<TableColumn
						key={column.uid}
						align={column.uid === "actions" ? "center" : "start"}
						allowsSorting={column.sortable}
					>
						{column.name}
					</TableColumn>
				)}
			</TableHeader>
			<TableBody
				emptyContent={"No projects found"}
				items={sortedItems}
			>
				{(item) => (
					<TableRow key={item.id}>
						{(columnKey) => (
							<TableCell>{renderCell(item, columnKey)}</TableCell>
						)}
					</TableRow>
				)}
			</TableBody>
		</Table>
	);
}
