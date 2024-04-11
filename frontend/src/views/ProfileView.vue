<script setup>
import { computed, ref, reactive } from "vue"
import { useAuthStore } from "@/stores/auth"
import { usePopupStore } from "@/stores/popup"
import { useApplicationStore } from "@/stores/application"
import ApplicationCard from "@/components/cards/ApplicationCard.vue"
import axios from "axios"

async function updateUser() {
    if (!isValid()) {
        return
    }

    const data = new FormData()
    data.append("phone", phone.value)
    data.append("email", email.value)

    try {
        await axios.put("/api/users/me", data, {
            headers: {
                Authorization: `Bearer ${authStore.accessToken}`
            }
        })
        await authStore.updateCurrentUser()
        isEdit.value = false
    }
    catch (e) {
        errors.email.push(...(e.response.data.Email || []))
        errors.phone.push(...(e.response.data.Phone || [])) 
    }
}

function isValid() {
    validateEmail()
    validatePhone()
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

    phone.value = phone.value.toString()

    if (phone.value.length === 0) {
        errors.phone.push("Поле обязательно для заполнения")
    }
    else if (phone.value.length != 11 || phone.value[0] != "7") {
        errors.phone.push("Некорректный телефон")
    }
}

const authStore = useAuthStore()
const popupStore = usePopupStore()
const applicationStore = useApplicationStore()

const isEdit = ref(false)
const phone = ref(authStore.currentUser.phone)
const email = ref(authStore.currentUser.email)
const errors = reactive({
    phone: [],
    email: []
})

const prettyPhone = computed(() => {
    return authStore.currentUser.phone.replace(/(\d{1})(\d{3})(\d{3})(\d{2})(\d{2})/g, "+$1 ($2) $3-$4-$5")
})

applicationStore.updateApplications()
</script>

<template>
    <div class="profile container">
        <div class="profile__image">
            <img src="@/assets/images/user.svg" alt="photo">
        </div>
        <form class="profile__info" @submit.prevent="updateUser">
            <h2 class="title">Личный кабинет</h2>
            <p class="profile__info__item">
                <span class="primary-color">Ваш телефон: </span>
                <span v-if="!isEdit">{{ prettyPhone }}</span>
                <input
                    v-else
                    class="user__edit-input"
                    type="number"
                    v-model="phone"
                    :class="{ error: errors.phone.length }"
                    @input="validatePhone"
                >
            </p>
            <p class="profile__info__item">
                <span class="primary-color">Ваш email: </span>
                <span v-if="!isEdit">{{ authStore.currentUser.email }}</span>
                <input
                    v-else
                    class="user__edit-input"
                    type="email"
                    v-model="email"
                    :class="{ error: errors.email.length }"
                    @input="validateEmail"
                >
            </p>
            <PrimaryButton v-if="!isEdit" type="button" @click="isEdit = true">Редактировать</PrimaryButton>
            <PrimaryButton v-else type="submit">Сохранить</PrimaryButton>
        </form>
    </div>
    <h1 class="cards__title title underscore">Ваши запросы</h1>
    <PrimaryButton class="cards__create" @click="popupStore.showCreate()">Создать запрос</PrimaryButton>
    <div class="cards container">
        <ApplicationCard v-for="application in applicationStore.applications" :application="application" />
    </div>
</template>

<style scoped>
.profile {
    display: flex;
    gap: 25px;
    flex-wrap: wrap;
}

.profile__image {
    width: 300px;
    height: 300px;
    background-color: var(--card-color);
    border-radius: 10px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.profile__image>img {
    width: 200px;
    height: 200px;
}

.profile__info {
    display: flex;
    gap: 25px;
    flex-direction: column;
    justify-content: center;
}

.profile__info__item {
    color: var(--text-color);
    font-size: 18px;
    font-weight: 400;
}

.cards__title {
    margin-top: 75px;
    margin-bottom: 25px;
}

.cards__create {
    display: block;
    margin: auto;
    margin-bottom: 50px;
}

.user__edit-input {
    color: var(--text-color);
    font-size: 18px;
    font-weight: 400;
    background-color: none;
    border: none;
    border-bottom: 1px solid var(--primary-color);
}

.user__edit-input:focus {
    outline: none;
}

.user__edit-input.error {
    border-bottom: 1px solid var(--error-color);
}
</style>
