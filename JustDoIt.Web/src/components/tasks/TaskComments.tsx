import { Accordion, AccordionItem } from "@nextui-org/react";
import React from "react";

interface Props {}

const TaskComments = () => {
	return (
		<Accordion>
			<AccordionItem
				key='1'
				aria-label='Comments'
				title='Comments'
			>
				{"Comment 1"}
			</AccordionItem>
		</Accordion>
	);
};

export default TaskComments;
