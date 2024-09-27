import { useState } from "react";

import { TaskResponse } from "../../types/Types";
import {
  Button,
  Divider,
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalHeader,
  useDisclosure,
} from "@nextui-org/react";
import TaskComments from "./TaskComments";
import TaskAttachments from "./TaskAttachments";
import UpdateTaskComponent from "./UpdateTaskComponent";

interface Props {
  title: string;
  state: string;
  tasks: TaskResponse[];
  roleName: string;
  authToken: string;
}

const TaskGroup = ({ title, state, tasks, roleName, authToken }: Props) => {
  return (
    <figure className="border p-2 flex-1">
      <figcaption className="p-2 text-center">{title}</figcaption>
      <ul className="flex flex-col gap-2">
        {tasks.map((task) => {
          if (task.state === state)
            return (
              <li className="border rounded-md" key={task?.id}>
                <UpdateTaskComponent
                  task={task}
                  roleName={roleName}
                  authToken={authToken}
                />
              </li>
            );
        })}
      </ul>
    </figure>
  );
};

export default TaskGroup;
