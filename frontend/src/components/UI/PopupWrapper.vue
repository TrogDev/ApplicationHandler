<script setup>
import { usePopupStore } from '@/stores/popup'

const popupStore = usePopupStore()
</script>

<template>
    <Teleport to="body">
        <div class="popup-wraper" @click="popupStore.hideAll()">
            <div class="popup" @click.stop>
                <slot></slot>
            </div>
        </div>
    </Teleport>
</template>

<style scoped>
.popup-wraper {
    background-color: rgba(0, 0, 0, 0.4);
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    display: flex;
    z-index: 20;
}

.popup {
    background-color: var(--background-color);
    border-top: 5px solid var(--primary-color);
    border-bottom: 5px solid var(--primary-color);
    border-radius: 15px;
    padding: 40px 20px;
    margin: auto;
    width: 500px;
}

@media (max-width: 520px) {
    .popup {
        width: 90%;
    }
}
</style>
