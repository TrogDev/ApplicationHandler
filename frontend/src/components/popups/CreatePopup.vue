<script setup>
import axios from "axios"
import { usePopupStore } from "@/stores/popup"
import { useApplicationStore } from "@/stores/application"
import { reactive, ref } from "vue"

import { useAuthStore } from "@/stores/auth"
import PrimaryButton from "../UI/PrimaryButton.vue"

const popupStore = usePopupStore()
const authStore = useAuthStore()
const applicationStore = useApplicationStore()

const title = ref("")
const description = ref("")
const errors = reactive({
    title: [],
    description: [],
})

async function create() {
    if (!isValid()) {
        return
    }

    const data = new FormData()
    data.append("title", title.value)
    data.append("description", description.value)

    await axios.post("/api/applications", data, {
        headers: {
            Authorization: `Bearer ${authStore.accessToken}`
        }
    })
    
    await applicationStore.updateApplications()
    popupStore.hideAll()

    title.value = ""
    description.value = ""
}

function isValid() {
    validateTitle()
    validateDescription()
    return !Object.entries(errors).some(([key, value]) => value.length)
}

function validateTitle() {
    errors.title.length = 0
    
    if (title.value.length === 0) {
        errors.title.push("Поле обязательно для заполнения")
    }
}

function validateDescription() {
    errors.description.length = 0

    if (description.value.length === 0) {
        errors.description.push("Поле обязательно для заполнения")
    }
}
</script>

<template>
    <PopupWrapper v-if="popupStore.isShowCreate">
        <form class="popup-form" @submit.prevent="create">
            <h1 class="popup-title">Создание запроса</h1>
            <div class="popup-form__inputs">
                <div class="popup-form__input-wrapper">
                    <PrimaryInput
                        placeholder="Краткое название . . ."
                        v-model="title"
                        :isError="!!errors.title.length"
                        @input="validateTitle"
                    />
                    <p class="error-text" v-if="errors.title.length">
                        {{ errors.title.join(", ") }}
                    </p>
                </div>
                <div class="popup-form__input-wrapper">
                    <PrimaryTextarea
                        placeholder="Опишите вашу задачу как можно подробнее . . ."
                        v-model="description"
                        :isError="!!errors.description.length"
                        @input="validateDescription"
                    />
                    <p class="error-text" v-if="errors.description.length">
                        {{ errors.description.join(", ") }}
                    </p>
                </div>
            </div>
            <PrimaryButton type="submit">Отправить запрос</PrimaryButton>
        </form>
    </PopupWrapper>
</template>
