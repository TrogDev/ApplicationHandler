<script setup>
import axios from 'axios'
import { computed, defineProps, defineEmits, reactive, ref } from 'vue'

const props = defineProps({
    application: {
        type: Object,
        required: true
    },
    isAdmin: {
        type: Boolean,
        default: false
    }
})

const emit = defineEmits(["accept", "cancel", "work", "complete", "updateDepartment"])

const statusName = computed(() => {
    return ["В обработке", "Отклонён", "Одобрен", "В работе", "Выполнен"][props.application.status - 1]
})

const department = ref(props.application.department.id)
const departments = reactive([])

async function updateDepartments() {
    const response = await axios.get("/api/departments")
    departments.length = 0
    departments.push(...response.data)
}

updateDepartments()
</script>

<template>
    <div class="card">
        <h2 class="title">{{ application.title }}</h2>
        <p class="card__description card__text">
            {{ application.description }}
        </p>
        <p class="card__text" v-if="isAdmin">
            <span class="primary-color">Отдел: </span>
            <select @change="emit('updateDepartment', application.id, department)" class="card__select card__text" v-model="department">
                <option v-for="department in departments" :value="department.id">{{ department.title }}</option>
            </select>
        </p>
        <p class="card__text" v-else>
            <span class="primary-color">Отдел: </span> {{ application.department.title }}
        </p>
        <p class="card__text">
            <span class="primary-color">Статус: </span> {{ statusName }}
        </p>
        <div class="admin-bar" v-if="isAdmin">
            <p class="card__text">
                <span class="primary-color">Email: </span> {{ application.user.email }}
            </p>
            <p class="card__text">
                <span class="primary-color">Телефон: </span> {{ application.user.phone }}
            </p>
            <template v-if="application.department.title != 'Отдел не определён'">
                <div class="buttons" v-if="application.status == 1">
                    <PrimaryButton @click="emit('accept', application.id)">Одобрить</PrimaryButton>
                    <PrimaryButton @click="emit('cancel', application.id)">Отклонить</PrimaryButton>
                </div>
                <div class="button" v-if="application.status == 3">
                    <PrimaryButton @click="emit('work', application.id)">Взять в работу</PrimaryButton>
                </div>
                <div class="button" v-if="application.status == 4">
                    <PrimaryButton @click="emit('complete', application.id)">Выполнено</PrimaryButton>
                </div>
            </template>
        </div>
    </div>
</template>

<style scoped>
.card {
    padding: 15px;
    border-radius: 10px;
    background-color: var(--card-color);
    display: flex;
    flex-direction: column;
    gap: 15px;
}

.card__description {
    max-width: 365px;
    height: 240px;
    overflow-y: scroll;
    -ms-overflow-style: none;
    scrollbar-width: none;
}

.card__description::-webkit-scrollbar {
    display: none;
}

.card__text {
    font-size: 18px;
    font-weight: 500;
    color: var(--text-color);
}

.card__select {
    border: none;
    background: none;
}

.card__select:focus {
    outline: none;
}

.admin-bar {
    width: 100%;
    border-top: 2px solid var(--primary-color);
    padding-top: 15px;
    display: flex;
    flex-direction: column;
    gap: 15px;
}

.buttons {
    display: flex;
    gap: 15px;
}
</style>