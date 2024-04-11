<template>
    <footer>
        <div class="footer-content container">
            <section>
                <RouterLink class="logo" to="/">
                    <img src="@/assets/images/logo.svg" alt="logo" class="wrapped-image">
                </RouterLink>
            </section>
            <section>
                <div class="footer__links">
                    <a href="https://1cbit.ru/" class="footer__contact">Официальный сайт</a>
                    <a href="https://vk.com/bit_it" class="footer__contact">VK</a>
                    <a href="https://www.youtube.com/user/1bitOfficial" class="footer__contact">YouTube</a>
                </div>
            </section>
            <section>
                <div class="footer__contacts">
                    <a href="tel:78612104343" class="footer__contact">+7 (861) 210-43-43</a>
                    <a href="tel:78612104343" class="footer__contact">krasnodar@1cbit.ru</a>
                </div>
            </section>
        </div>
        <p class="footer-copy">
            © ООО «Первый Бит» ОГРН 1177746107362 ИНН 7728361580
        </p>
    </footer>
</template>

<style scoped>
footer {
    background-color: var(--card-color);
    margin-top: 75px;
    padding: 40px 0 20px 0;
}

.footer-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 40px;
    flex-wrap: wrap;
}

@media (max-width: 800px) {
    .footer-content {
        justify-content: center;
    }
}

.footer__links {
    display: flex;
    gap: 30px;
}

.footer__contacts {
    display: flex;
    flex-direction: column;
    gap: 15px;
}

.footer__contact {
    font-size: 18px;
    font-weight: 500;
    color: var(--text-color);
    text-decoration: none;
}

.footer-copy {
    font-size: 16px;
    color: var(--placeholder-color);
    width: 100%;
    text-align: center;
    margin-top: 25px;
}
</style>