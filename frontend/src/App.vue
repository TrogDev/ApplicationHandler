<script setup>
import Header from './components/partials/Header.vue'
import Popups from './components/partials/Popups.vue'
import Footer from './components/partials/Footer.vue'
</script>

<template>
  <Header></Header>
  <main>
    <RouterView />
  </main>
  <Footer></Footer>
  <Popups></Popups>
</template>
