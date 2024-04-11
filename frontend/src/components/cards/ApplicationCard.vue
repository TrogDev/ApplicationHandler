<script setup>
import { computed, defineProps, defineEmits } from 'vue'

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

const emit = defineEmits(["accept", "cancel", "work", "complete"])

const statusName = computed(() => {
    return ["В обработке", "Отклонён", "Одобрен", "В работе", "Выполнен"][props.application.status - 1]
})
</script>

<template>
    <div class="card">
        <h2 class="title">{{ application.title }}</h2>
        <p class="card__description card__text">
            {{ application.description }}
        </p>
        <p class="card__text">
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