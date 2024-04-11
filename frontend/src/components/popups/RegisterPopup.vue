<script setup>
import axios from "axios"
import { usePopupStore } from "@/stores/popup"
import { reactive, ref } from "vue"

import { useAuthStore } from "@/stores/auth"
import PrimaryButton from "../UI/PrimaryButton.vue"

const popupStore = usePopupStore()
const authStore = useAuthStore()

const email = ref("")
const phone = ref("")
const password = ref("")
const passwordRepeat = ref("")
const errors = reactive({
    email: [],
    phone: [],
    password: [],
    passwordRepeat: []
})

async function register() {
    if (!isValid()) {
        return
    }

    const data = new FormData()
    data.append("email", email.value)
    data.append("phone", phone.value)
    data.append("password", password.value)

    try {
        const response = await axios.post("/api/auth/register", data)
        authStore.updateAccessToken(response.data.accessToken.token)
        popupStore.hideAll()
    }
    catch (e) {
        errors.email.push(...(e.response.data.Email || []))
        errors.phone.push(...(e.response.data.Phone || [])) 
        errors.password.push(...(e.response.data.Password || [])) 
    }
}

function isValid() {
    validateEmail()
    validatePhone()
    validatePassword()
    validatePasswordRepeat()
    return !Object.entries(errors).some(([key, value]) => value.length)
}

function validateEmail() {
    errors.email.length = 0
    
    if (email.value.length === 0) {
        errors.email.push("Поле обязательно для заполнения")
    }
    else if (!email.value.match(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)) {
        errors.email.push("Некорректный email")
    }
}

function validatePhone() {
    errors.phone.length = 0

    if (phone.value.length === 0) {
        errors.phone.push("Поле обязательно для заполнения")
    }
    else if (phone.value.length != 11 || phone.value[0] != "7") {
        errors.phone.push("Некорректный телефон")
    }
}

function validatePassword() {
    errors.password.length = 0
    validatePasswordRepeat()

    if (password.value.length === 0) {
        errors.password.push("Поле обязательно для заполнения")
    }
    else if (password.value.length < 8) {
        errors.password.push("Пароль слишком короткий")
    }
}

function validatePasswordRepeat() {
    errors.passwordRepeat.length = 0

    if (passwordRepeat.value.length === 0) {
        errors.passwordRepeat.push("Поле обязательно для заполнения")
    }
    else if (passwordRepeat.value != password.value) {
        errors.passwordRepeat.push("Пароли не совпадают")
    }
}
</script>

<template>
    <PopupWrapper v-if="popupStore.isShowRegister">
        <form class="popup-form" @submit.prevent="register">
            <h1 class="popup-title">Регистрация</h1>
            <div class="popup-form__inputs">
                <div class="popup-form__input-wrapper">
                    <PrimaryInput
                        placeholder="Почта"
                        v-model="email"
                        :isError="!!errors.email.length"
                        @input="validateEmail"
                    />
                    <p class="error-text" v-if="errors.email.length">
                        {{ errors.email.join(", ") }}
                    </p>
                </div>
                <div class="popup-form__input-wrapper">
                    <PrimaryInput
                        placeholder="Телефон"
                        type="number"
                        v-model="phone" 
                        :isError="!!errors.phone.length"
                        @input="validatePhone"
                    />
                    <p class="error-text" v-if="errors.phone.length">
                        {{ errors.phone.join(", ") }}
                    </p>
                </div>
                <div class="popup-form__input-wrapper">
                    <PrimaryInput
                        type="password"
                        placeholder="Пароль"
                        v-model="password"
                        :isError="!!errors.password.length"
                        @input="validatePassword"
                    />
                    <p class="error-text" v-if="errors.password.length">
                        {{ errors.password.join(", ") }}
                    </p>
                </div>
                <div class="popup-form__input-wrapper">
                    <PrimaryInput
                        type="password"
                        placeholder="Повторите пароль"
                        v-model="passwordRepeat"
                        :isError="!!errors.passwordRepeat.length"
                        @input="validatePasswordRepeat"
                    />
                    <p class="error-text" v-if="errors.passwordRepeat.length">
                        {{ errors.passwordRepeat.join(", ") }}
                    </p>
                </div>
            </div>
            <PrimaryButton type="submit">Создать аккаунт</PrimaryButton>
            <p class="popup-link" @click="popupStore.showLogin()">Уже есть аккаунт?</p>
        </form>
    </PopupWrapper>
</template>
