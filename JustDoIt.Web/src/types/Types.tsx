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
}
