import { Accordion, AccordionItem } from "@nextui-org/react";
import React from "react";

interface Props {}

const TaskComments = () => {
	return (
		<Accordion>
			<AccordionItem
				key='1'
				aria-label='Accordion 1'
				title='Accordion 1'
			>
				{"hello"}
			</AccordionItem>
		</Accordion>
	);
};

export default TaskComments;
