<template>
    <button class="primary-button">
        <slot></slot>
    </button>
</template>

<style scoped>
.primary-button {
    padding: 8px 20px;
    background-color: var(--primary-color);
    border: none;
    border-radius: 10px;
    color: var(--background-color);
    font-size: 18px;
    font-weight: 500;
    cursor: pointer;
}
</style>
