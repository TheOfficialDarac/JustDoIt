import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
} from "@nextui-org/react";

type Props = {
  name: string;
};

const Footer = ({ name }: Props) => {
  // console.log(name);
  return (
    <>
      {/* <footer className="container bg-teal-100 mx-auto mt-2 flex-initial max-w-screen-xl p-2">
        this is {name}
      </footer> */}
      {/* <Table
        aria-label="Example static collection table"
        radius="none"
        className="max-w-screen-xl p-2 mx-auto"
        shadow="none"
        align="center"
      >
        <TableHeader className="bg-transparent">
          <TableColumn align="center">Contact</TableColumn>
          <TableColumn align="center">Contact</TableColumn>
          <TableColumn align="center">Contact</TableColumn>
        </TableHeader>
        <TableBody>
          <TableRow key="1">
            <TableCell>Test</TableCell>
            <TableCell>Test</TableCell>
            <TableCell>Test</TableCell>
          </TableRow>
          <TableRow key="2">
            <TableCell>Test</TableCell>
            <TableCell>Test</TableCell>
            <TableCell>Test</TableCell>
          </TableRow>
        </TableBody>
      </Table> */}
    </>
  );
};

export default Footer;
