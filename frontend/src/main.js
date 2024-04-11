import './assets/css/main.css'
import UI from "@/components/UI"

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import router from './router'

import App from './App.vue'

import { useAuthStore } from "@/stores/auth"

const app = createApp(App)

UI.forEach(e => app.component(e.name, e.component))

app.use(createPinia())

app.use(router)

app.mount('#app')
