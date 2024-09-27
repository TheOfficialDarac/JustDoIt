import { fileURLToPath, URL } from "node:url";

import { defineConfig } from "vite";
import plugin from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";

const baseFolder =
	env.APPDATA !== undefined && env.APPDATA !== ""
		? `${env.APPDATA}/ASP.NET/https`
		: `${env.HOME}/.aspnet/https`;

const certificateName = "JustDoIt.Web";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
	if (
		0 !==
		child_process.spawnSync(
			"dotnet",
			[
				"dev-certs",
				"https",
				"--export-path",
				certFilePath,
				"--format",
				"Pem",
				"--no-password",
			],
			{ stdio: "inherit" }
		).status
	) {
		throw new Error("Could not create certificate.");
	}
}

const target = "https://localhost:7010";
// "http://localhost:5153";

// env.ASPNETCORE_HTTPS_PORT
//   ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
//   : env.ASPNETCORE_URLS
//   ? env.ASPNETCORE_URLS.split(";")[0]
// https:vitejs.dev/config/

export default defineConfig({
	plugins: [plugin()],
	resolve: {
		alias: {
			"@": fileURLToPath(new URL("./src", import.meta.url)),
		},
	},
	server: {
		proxy: {
			"^/api/v1/auth/login": {
				target,
				secure: false,
			},
			"^/api/v1/auth/register": {
				target,
				secure: false,
			},
			"^/api/v1/auth/data": {
				target,
				secure: false,
			},
			"^/api/v1/auth/update": {
				target,
				secure: false,
			},
			"/api/v1/projects/user": {
				target,
				secure: false,
			},
			"/api/v1/projects/user-role": {
				target,
				secure: false,
			},
			"/api/v1/projects/create": {
				target,
				secure: false,
			},
			"/api/v1/projects/update": {
				target,
				secure: false,
			},
			"/api/v1/projects/delete": {
				target,
				secure: false,
			},
			"/api/v1/tasks": {
				target,
				secure: false,
			},
			"/api/v1/tasks/create": {
				target,
				secure: false,
			},
			"/api/v1/tasks/update": {
				target,
				secure: false,
			},
			"/api/v1/attachments": {
				target,
				secure: false,
			},
			"/api/v1/attachments/get-file": {
				target,
				secure: false,
			},
			"/api/v1/attachments/task-attachments": {
				target,
				secure: false,
			},
		},
		port: 5173,
		https: {
			key: fs.readFileSync(keyFilePath),
			cert: fs.readFileSync(certFilePath),
		},
	},
});

// import { defineConfig } from 'vite'
// import react from '@vitejs/plugin-react'

// // https://vitejs.dev/config/
// export default defineConfig({
//   plugins: [react()],
// })
