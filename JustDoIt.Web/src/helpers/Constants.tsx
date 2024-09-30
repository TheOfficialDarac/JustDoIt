import { ChipProps } from "@nextui-org/react";

export const projectStatusColorMap: Record<string, ChipProps["color"]> = {
  Active: "success",
  Inactive: "danger",
  Suspended: "warning",
};
