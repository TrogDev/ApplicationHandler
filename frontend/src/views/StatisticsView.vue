<script setup>
import { reactive, computed } from 'vue'
import { Pie, Bar } from 'vue-chartjs'
import axios from 'axios'
import { useAuthStore } from '@/stores/auth'
import { Chart as ChartJS, ArcElement, Tooltip, Legend, CategoryScale, LinearScale, BarElement, Title } from 'chart.js'

ChartJS.register(ArcElement, Tooltip, Legend, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend)

const authStore = useAuthStore()

const applications = reactive([])

async function updateApplications() {
    const response = await axios.get("/api/admin/applications", {
        headers: {
            Authorization: `Bearer ${authStore.accessToken}`
        }
    })
    applications.length = 0
    applications.push(...response.data)
}

updateApplications()

function groupBy(list, keyGetter) {
    const map = new Map();
    list.forEach((item) => {
        const key = keyGetter(item);
        const collection = map.get(key);
        if (!collection) {
            map.set(key, [item]);
        } else {
            collection.push(item);
        }
    });
    return map;
}

const applicationsByDepartmentData = computed(() => {
    const groupedApplications = groupBy(applications, app => app.department.title)
    const data = {
        labels: [],
        datasets: [
            {
                backgroundColor: ["#352C85", "#c43434", "#707070", "#defd6b", "#9900FF"],
                data: []
            }
        ]
    }

    for (let [dep, apps] of groupedApplications) {
        data.labels.push(dep)
        data.datasets[0].data.push(apps.length)
    }

    return data
})

const applicationsByStatusData = computed(() => {
    const groupedApplications = groupBy(applications, app => app.status)
    const data = {
        labels: [],
        datasets: [
            {
                backgroundColor: ["#352C85", "#c43434", "#707070", "#defd6b", "#9900FF"],
                data: []
            }
        ]
    }

    for (let [status, apps] of groupedApplications) {
        data.labels.push(["В обработке", "Отклонён", "Одобрен", "В работе", "Выполнен"][status - 1])
        data.datasets[0].data.push(apps.length)
    }

    return data
})

const applicationsByDayData = computed(() => {
    const groupedApplications = groupBy(applications, app => {
        let createdDate = new Date(app.createdAt)
        return `${createdDate.getDate()}/${createdDate.getMonth() + 1}/${createdDate.getFullYear()}`
    })

    const data = {
        labels: [],
        datasets: [
            {
                label: "",
                backgroundColor: ["#352C85", "#c43434", "#707070", "#defd6b", "#9900FF"],
                data: []
            }
        ]
    }

    for (let [date, apps] of groupedApplications) {
        data.labels.unshift(date)
        data.datasets[0].data.unshift(apps.length)
    }

    return data
})

const currentMonthApplications = computed(() => {
    return applications.filter(app => {
        return new Date().getMonth() == new Date(app.createdAt).getMonth()
        && new Date().getFullYear() == new Date(app.createdAt).getFullYear()
    })
})

const currentDayApplications = computed(() => {
    return applications.filter(app => {
        return new Date().getMonth() == new Date(app.createdAt).getMonth()
        && new Date().getFullYear() == new Date(app.createdAt).getFullYear()
        && new Date().getDate() == new Date(app.createdAt).getDate()
    })
})
</script>

<template>
    <h1 class="title underscore">Статистика запросов</h1>
    <div class="statistics container">
        <section>
            <h1 class="title">Запросы по отделам</h1>
            <div class="pie">
                <Pie :data="applicationsByDepartmentData" :options="{ responsive: true }" />
            </div>
        </section>
        <section>
            <h1 class="title">Запросы по статусам</h1>
            <div class="pie">
                <Pie :data="applicationsByStatusData" :options="{ responsive: true }" />
            </div>
        </section>
        <section>
            <h1 class="title">Запросы по дням</h1>
            <div class="pie">
                <Bar :data="applicationsByDayData" :options="{ responsive: true }" />
            </div>
        </section>
        <section>
            <h1 class="title">Общее количество запросов</h1>
            <p class="statistics__text"><span class="primary-color">За всё время:</span> {{ applications.length }}</p>
            <p class="statistics__text"><span class="primary-color">За этот месяц:</span> {{ currentMonthApplications.length }}</p>
            <p class="statistics__text"><span class="primary-color">За сегодня:</span> {{ currentDayApplications.length }}</p>
        </section>
    </div>
</template>

<style scoped>
.pie {
    width: 400px;
    height: 400px;
    margin-top: 30px;
}

section {
    text-align: center;
    margin: auto;
    margin-top: 75px;
    width: fit-content;
}

.statistics {
    display: flex;
    gap: 60px;
    flex-wrap: wrap;
    align-items: center;
}

.statistics__text {
    font-size: 18px;
    font-weight: 500;
    margin-top: 10px;
}
</style>