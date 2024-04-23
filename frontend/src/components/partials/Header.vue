<script setup>
import { useAuthStore } from '@/stores/auth'
import { usePopupStore } from "@/stores/popup"
import { ref } from "vue"
import PrimaryButton from '../UI/PrimaryButton.vue'

const authStore = useAuthStore()
const popupStore = usePopupStore()

const isShowMenu = ref(false)

function toggleMenu() {
    isShowMenu.value = !isShowMenu.value
}
</script>

<template>
    <header v-if="authStore.isInit" class="container">
        <section>
            <RouterLink class="logo" to="/">
                <img src="@/assets/images/logo.svg" alt="logo" class="wrapped-image">
            </RouterLink>
        </section>
        <section v-if="authStore.isAuth" class="user" @click="toggleMenu">
            <div class="user-image">
                <img src="@/assets/images/user.svg" alt="image" class="wrapped-image">
            </div>
            <p class="user-email">
                {{ authStore.currentUser.email }}
            </p>
            <div class="user-down" :class="{ active : isShowMenu }">
                <img src="@/assets/images/down.svg" alt="image" class="wrapped-image">
            </div>
            <div class="user-menu" v-if="isShowMenu">
                <RouterLink class="user-menu__item" to="/admin" v-if="authStore.currentUser.role == 2">Админ панель</RouterLink>
                <RouterLink class="user-menu__item" to="/statistics" v-if="authStore.currentUser.role == 2">Статистика</RouterLink>
                <RouterLink class="user-menu__item" to="/lk" v-else>Личный кабинет</RouterLink>
                <p class="user-menu__item" @click="authStore.logout()">Выйти</p>
            </div>
        </section>
        <section v-else>
            <button class="login-button" @click="popupStore.showLogin">Вход</button>
            <PrimaryButton @click="popupStore.showRegister">Регистрация</PrimaryButton>
        </section>
    </header>
</template>

<style scoped>
header {
    margin-top: 20px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 30px;
    flex-wrap: wrap;
}

@media (max-width: 530px) {
    header {
        justify-content: center;
    }
}

header>section {
    display: flex;
    gap: 10px;
    align-items: center;
}

.login-button {
    padding: 0 10px;
    background: none;
    border: none;
    border-radius: 10px;
    color: var(--text-color);
    font-size: 18px;
    font-weight: 500;
    cursor: pointer;
}

.user {
    cursor: pointer;
    position: relative;
}

.user-image {
    width: 35px;
    height: 35px;
}

.user-email {
    color: var(--text-color);
    font-size: 18px;
    font-weight: 500;
}

.user-down {
    width: 18px;
    height: 15px;
    transition: all .2s ease;
}

.user-down.active {
    transform: rotate(180deg);
}

.user-menu {
    position: absolute;
    width: 200px;
    top: 50px;
    right: 0;
    background-color: var(--card-color);
    border: 2px solid var(--primary-color);
    border-radius: 5px;
}

.user-menu__item {
    padding: 10px 0;
    display: flex;
    justify-content: center;
    text-decoration: none;
    color: var(--text-color);
    transition: all .2s ease;
}

.user-menu__item:hover {
    background-color: var(--primary-color);
    color: var(--background-color);
}
</style>