import { ref } from 'vue'
import { defineStore } from 'pinia'

export const usePopupStore = defineStore('popup', () => {
    const isShowLogin = ref(false)
    const isShowRegister = ref(false)
    const isShowCreate = ref(false)


    function showLogin() {
        isShowLogin.value = true
        isShowRegister.value = false
        isShowCreate.value = false
    }

    function showRegister() {
        isShowLogin.value = false
        isShowRegister.value = true
        isShowCreate.value = false
    }

    function showCreate() {
        isShowLogin.value = false
        isShowRegister.value = false
        isShowCreate.value = true
    }

    function hideAll() {
        isShowLogin.value = false
        isShowRegister.value = false
        isShowCreate.value = false
    }

    return {
        isShowLogin,
        isShowRegister,
        isShowCreate,
        showLogin,
        showRegister,
        showCreate,
        hideAll
    }
})
