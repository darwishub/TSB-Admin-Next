import { createApp } from 'vue'
import Router from './routes'
import App from './App.vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import axios from 'axios'
import VueAxios from 'vue-axios'
import VueGapi from 'vue-gapi'
import VueCookies from 'vue-cookies'
import { autoAnimatePlugin } from '@formkit/auto-animate/vue'
import ResizeTextarea from 'resize-textarea-vue3'
import VueAwesomePaginate from "vue-awesome-paginate";
import VueSweetalert2 from 'vue-sweetalert2';
import { VueSignalR } from '@dreamonkey/vue-signalr';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { ProCalendar } from "vue-pro-calendar";

/*Bootstrap*/
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap/dist/js/bootstrap.min.js"
import 'bootstrap-icons/font/bootstrap-icons.css'

import "vue-awesome-paginate/dist/style.css";
import 'sweetalert2/dist/sweetalert2.min.css';
import 'maz-ui/css/main.css'

/*Global CSS styles*/
import '@/assets/css/styles.css'
import '@/assets/font/thaleah/stylesheet.css'
import '@/interceptors/index'

const connection = new HubConnectionBuilder()
    .withUrl("" + location.origin + "/api/ChatHub")
    .withAutomaticReconnect()
    .build();

const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);

const app = createApp(App);
app.use(Router)
app.use(pinia)
app.use(ResizeTextarea)
app.use(VueSweetalert2)
app.use(VueAwesomePaginate)
app.use(VueAxios, axios)
app.use(VueCookies)
app.use(autoAnimatePlugin)
app.use(ProCalendar)

app.use(VueSignalR, {
    connection,
    failFn: () => {
        console.log("failed to connect")
    },
})

app.provide('axios', app.config.globalProperties.axios)
// app.use(VueGapi, {
//     clientId: '770991724885-ttkh7j3mk47onf4bq0r3am04e6vtt48r.apps.googleusercontent.com',
//     scope: 'https://www.googleapis.com/auth/userinfo.email',
// })
app.use(VueGapi, {
    clientId: '770991724885-ttkh7j3mk47onf4bq0r3am04e6vtt48r.apps.googleusercontent.com',
    scope: 'https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/calendar.readonly',
  })
app.mount("#app");
