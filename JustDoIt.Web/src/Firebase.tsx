// Import the functions you need from the SDKs you need

import { initializeApp } from "firebase/app";

// TODO: Add SDKs for Firebase products that you want to use

// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration

const firebaseConfig = {
  apiKey: "AIzaSyDaibpYb8AaT-xs4zbiBKz8PVNArBL5yJg",

  authDomain: "task-manager-just-do-it.firebaseapp.com",

  databaseURL:
    "https://task-manager-just-do-it-default-rtdb.europe-west1.firebasedatabase.app",

  projectId: "task-manager-just-do-it",

  storageBucket: "task-manager-just-do-it.appspot.com",

  messagingSenderId: "979009761286",

  appId: "1:979009761286:web:1ad8f3f4498b8f14a81387",
};

// Initialize Firebase

export const firebaseApp = initializeApp(firebaseConfig);
