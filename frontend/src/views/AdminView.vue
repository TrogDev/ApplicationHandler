<script setup>
import axios from "axios"
import { reactive, ref } from "vue"
import { useAuthStore } from "@/stores/auth"
import ApplicationCard from "@/components/cards/ApplicationCard.vue"

const authStore = useAuthStore()

const departments = reactive([])
const applications = reactive([])

const departmentId = ref(1);
const status = ref(1);

async function updateDepartments() {
    const response = await axios.get("/api/departments")
    departments.length = 0
    departments.push(...response.data)
}

async function updateApplications() {
    const response = await axios.get("/api/admin/applications", {
        params: {
            departmentId: departmentId.value,
            status: status.value
        },
        headers: {
            Authorization: `Bearer ${authStore.accessToken}`
        }
    })
    applications.length = 0
    applications.push(...response.data)
}

async function updateStatus(id, status) {
    const data = new FormData()
    data.append("status", status)

    await axios.put(`/api/admin/applications/${id}/status`, data, {
        headers: {
            Authorization: `Bearer ${authStore.accessToken}`
        }
    })

    await updateApplications()
}

async function updateDepartment(id, departmentId) {
    const data = new FormData()
    data.append("departmentId", departmentId)

    await axios.put(`/api/admin/applications/${id}/department`, data, {
        headers: {
            Authorization: `Bearer ${authStore.accessToken}`
        }
    })

    await updateApplications()
}

updateDepartments()
updateApplications()
</script>

<template>
    <h1 class="title underscore">Просмотр запросов</h1>
    <div class="filter">
        <select class="filter__select" v-model="departmentId" @change="updateApplications">
            <option v-for="department in departments" :value="department.id">{{ department.title }}</option>
        </select>
        <select class="filter__select" v-model="status" @change="updateApplications">
            <option value="1">В обработке</option>
            <option value="2">Отклонёные</option>
            <option value="3">Одобреные</option>
            <option value="4">В работе</option>
            <option value="5">Выполненные</option>
        </select>
    </div>
    <div class="cards container">
        <ApplicationCard
            v-for="application in applications"
            :application="application"
            :isAdmin="true"
            :key="application.id"
            @cancel="updateStatus(application.id, 2)"
            @accept="updateStatus(application.id, 3)"
            @work="updateStatus(application.id, 4)"
            @complete="updateStatus(application.id, 5)"
            @updateDepartment="updateDepartment"
        />
    </div>
</template>

<style scoped>
.filter {
    display: flex;
    justify-content: center;
    gap: 25px;
    margin-top: 25px;
    flex-wrap: wrap;
}

.filter__select {
    height: 40px;
    border-radius: 10px;
    border: 2px solid var(--primary-color);
    width: 250px;
    padding: 0 15px;
    -webkit-appearance: none;
    -moz-appearance: none;
    text-indent: 1px;
    text-overflow: '';
}

.filter__select:focus {
    outline: none;
    border: 3px solid var(--primary-color);
}

.cards {
    margin-top: 75px;
}
</style>