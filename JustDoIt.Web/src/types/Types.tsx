export interface AuthResponse {
	authToken: string;
}

export interface UserResponse {
	email: string;
	firstName: string;
	lastName: string;
	phoneNumber: string;
	pictureUrl: string;
	userName: string;
}

export interface ProjectResponse {
	id: number;
	title: string;
	pictureUrl: string;
	isActive: boolean;
	createdDate: string;
	description: string;
}

export interface TaskResponse {
	id: number;
	projectId: number;
	issuerId: string;
	title: string;
	description: string;
	summary: string;
	pictureUrl: string;
	deadline: string;
	createdDate: string;
	isActive: boolean;
	state: string;
}

export interface ProjectRoleResponse {
	roleName: string;
	roleDescription: string;
}

export interface TaskAttachmentResponse {
	id: number;
	taskId: number;
	filepath: string;
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
