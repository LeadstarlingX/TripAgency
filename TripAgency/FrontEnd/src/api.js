import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:7070/api",
    //withCredentials: false,
    headers: {
        'Accept': "application/json",
        'Content-Type': "application/json",
    },
});

export default api;
