import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'
import { useRouter } from "vue-router"

export const useAuthStore = defineStore('auth', () => {
  const router = useRouter()
  const isInit = ref(false)
  const accessToken = ref(localStorage.accessToken)
  const currentUser = ref(null)

  const isAuth = computed(() => {
    return currentUser.value != null
  })

  async function updateCurrentUser() {
    if (accessToken.value) {
      currentUser.value = await getCurrentUser()
    }
    else {
      currentUser.value = null
    }
    isInit.value = true
  }

  async function updateAccessToken(token) {
    localStorage.accessToken = token
    accessToken.value = token
    await updateCurrentUser()
  }

  async function getCurrentUser() {
    const response = await axios.get("/api/users/me", {
      headers: {
        "Authorization": `Bearer ${accessToken.value}`
      }
    })
    return response.data
  }

  function logout() {
    delete localStorage.accessToken
    accessToken.value = null
    updateCurrentUser()
    router.push('/')
  }

  return { isInit, isAuth, currentUser, accessToken, updateCurrentUser, updateAccessToken, logout }
})
