<script setup>
import axios from "axios"
import { usePopupStore } from "@/stores/popup"
import { reactive, ref } from "vue"

import { useAuthStore } from "@/stores/auth"
import PrimaryButton from "../UI/PrimaryButton.vue"

const popupStore = usePopupStore()
const authStore = useAuthStore()

const email = ref("")
const password = ref("")
const errors = reactive({
    email: [],
    password: [],
})

async function login() {
    clearErrors()
    const data = new FormData()
    data.append("email", email.value)
    data.append("password", password.value)

    try {
        const response = await axios.post("/api/auth/login", data)
        authStore.updateAccessToken(response.data.accessToken.token)
        popupStore.hideAll()
    }
    catch (e) {
        errors.email.push("Неверный логин или пароль")
        errors.password.push("Неверный логин или пароль") 
    }
}

function clearErrors() {
    errors.email.length = 0
    errors.password.length = 0
}
</script>

<template>
    <PopupWrapper v-if="popupStore.isShowLogin">
        <form class="popup-form" @submit.prevent="login">
            <h1 class="popup-title">Вход</h1>
            <div class="popup-form__inputs">
                <div class="popup-form__input-wrapper">
                    <PrimaryInput
                        placeholder="Почта"
                        v-model="email"
                        :isError="!!errors.email.length"
                        @input="clearErrors"
                    />
                    <p class="error-text" v-if="errors.email.length">
                        {{ errors.email.join(", ") }}
                    </p>
                </div>
                <div class="popup-form__input-wrapper">
                    <PrimaryInput
                        type="password"
                        placeholder="Пароль"
                        v-model="password"
                        :isError="!!errors.password.length"
                        @input="clearErrors"
                    />
                    <p class="error-text" v-if="errors.password.length">
                        {{ errors.password.join(", ") }}
                    </p>
                </div>
            </div>
            <PrimaryButton type="submit">Войти</PrimaryButton>
            <p class="popup-link" @click="popupStore.showRegister()">Нет учётной записи?</p>
        </form>
    </PopupWrapper>
</template>
