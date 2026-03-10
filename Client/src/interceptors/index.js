import axios from 'axios'
import Router from '../routes'

let refresh = false;

axios.interceptors.response.use(resp => resp, async error => {

    if (error.response.status === 401 && !refresh) {
        refresh = true;
        const { status, data } = await axios.post('api/Auth/refresh', {}, {
            withCredentials: true
        });

        if(status === 200) {
            axios.defaults.headers.common['authorization'] = `Bearer ${data}`
            return axios(error.config)
        }

        localStorage.removeItem("adminAuth");
        window.location.href = window.location.origin + '/signin'
    }
    refresh = false;
    return error;
});

