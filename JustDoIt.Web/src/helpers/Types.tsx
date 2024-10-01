export interface AuthResponse {
  authToken: string;
}

export interface User {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  pictureUrl: string;
  userName: string;
}

export interface Project {
  id: number;
  title: string;
  key: string;
  description: string;
  pictureUrl: string;
  statusId: number;
  createdDate: string;
}

export interface Category {
  id: number;
  value: string;
  description: string;
}

export interface Status {
  Id: string | number;
  id: number;
  tag: string;
  value: string;
}

export interface Task {
  id: number;
  issuerId: string;
  summary: string;
  description: string;
  projectId: number;
  pictureUrl: string;
  deadline: string;
  createdDate: string;
  lastChangeDate: string;
  priorityId: number;
  stateId: number;
  statusId: number;
}

export interface ProjectRole {
  roleName: string;
  roleDescription: string;
}

export interface Attachment {
  id: number;
  taskId: number;
  filepath: string;
}

export interface State {
  id: number;
  tag: string;
  value: string;
}

export interface Status {
  id: number;
  tag: string;
  value: string;
}

export interface Priority {
  id: number;
  tag: string;
  value: string;
}

export function parseJwt(token: string) {
  if (token) {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    return JSON.parse(jsonPayload);
  }
  return {};
}
