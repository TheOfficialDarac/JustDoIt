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
