import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import '@babel/polyfill'
import 'mutationobserver-shim'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

createApp(App).use(router).mount('#app')
