import { ChipProps } from "@nextui-org/react";

export const projectStatusColorMap: Record<string, ChipProps["color"]> = {
	active: "success",
	inactive: "danger",
	suspended: "warning",
};
