<script setup>
import { usePopupStore } from '@/stores/popup'
import { useAuthStore } from '@/stores/auth'

const popupStore = usePopupStore()
const authStore = useAuthStore()

function create() {
    if (authStore.isAuth) {
        popupStore.showCreate()
    }
    else {
        popupStore.showRegister()
    }
}
</script>

<template>
    <section class="container promo">
        <div class="promo__info">
            <h1 class="title">Система обработки ваших запросов</h1>
            <p class="promo__text">
                На данном сайте вы можете оставлять запросы на автоматизацию ваших бизнес задач для компании “Первый
                бит”.
                Система сохранит вашу заявку и направит её в нужный отдел, после этого с вами свяжутся в ближайшее
                время.
            </p>
            <PrimaryButton @click="create" class="promo__button">Создать запрос</PrimaryButton>
        </div>
        <div class="promo__image">
            <img src="@/assets/images/promo.png" alt="promo" class="wrapped-image"x>
        </div>
    </section>
    <section class="container about">
        <div class="about__images">
            <img src="@/assets/images/about.png" alt="promo" class="wrapped-image">
        </div>
        <div class="about__info">
            <h1 class="title underscore">Кто мы?</h1>
            <p class="about__text">
                Первый Бит в Краснодаре – интегратор 1С в регионе. Компания обладает ключевыми статусами,
                подтверждающими экспертизу команды, и занимает лидирующие позиции в различных рейтингах.
                Подробнее о нас вы можете посмотреть на нашем официальном сайте
            </p>
            <a href="https://1cbit.ru/" target="_blank"><PrimaryButton>Узнать больше</PrimaryButton></a>
        </div>
    </section>
</template>

<style scoped>
.promo {
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-wrap: wrap;
    gap: 40px;
}

@media (max-width: 1200px) {
    .promo {
        justify-content: center;
    }

    .promo__info {
        text-align: center;
        justify-content: center;
        align-items: center;
    }
}

.promo__info {
    display: flex;
    flex-direction: column;
    gap: 20px;
    padding: 10px;
}

.promo__text {
    font-size: 18px;
    line-height: 30px;
    font-weight: 500;
    color: var(--text-color);
    max-width: 390px;
}

.promo__button {
    width: fit-content;
}

.about {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-top: 100px;
    gap: 40px;
    flex-wrap: wrap;
}

@media (max-width: 1270px) {
    .about {
        flex-direction: column-reverse;
    }
}

.about__info {
    display: flex;
    flex-direction: column;
    gap: 20px;
    align-items: center;
}

.about__text {
    max-width: 530px;
    text-align: center;
    font-size: 18px;
    line-height: 30px;
    font-weight: 500;
    color: var(--text-color);
}

.promo__image {
    max-width: 513px;
}

.about__images {
    max-width: 567px;
}
</style>
