import { createApp } from 'vue'
import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import '@babel/polyfill'
import 'mutationobserver-shim'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'
axios.defaults.withCredentials = true

createApp(App).use(store).use(router).mount('#app')



