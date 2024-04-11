import { ref, reactive } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'
import { useAuthStore } from './auth'

export const useApplicationStore = defineStore('application', () => {
    const authStore = useAuthStore()

    const applications = reactive([])

    async function updateApplications() {
        const response = await axios.get("/api/applications", {
            headers: {
                Authorization: `Bearer ${authStore.accessToken}`
            }
        })
        applications.length = 0
        applications.push(...response.data)
    }

    return { applications, updateApplications }
})
