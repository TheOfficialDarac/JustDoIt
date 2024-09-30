// tailwind.config.js
const { nextui } = require("@nextui-org/react");

/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
    "./node_modules/react-files-preview/dist/*.js",
    "./node_modules/@nextui-org/theme/dist/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    fontFamily: {
      fredoka: ["Fredoka", "Cascadia Mono", "sans-serif", "sans"],
    },
    extend: {},
  },
  darkMode: "class",
  plugins: [nextui()],
};
