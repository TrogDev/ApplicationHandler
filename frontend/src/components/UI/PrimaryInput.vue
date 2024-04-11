<script setup>
import { defineProps, ref, defineEmits } from "vue"

const emit = defineEmits(["update:modelValue"])

const props = defineProps({
    isError: {
        type: Boolean,
        default: false
    }
})

const modelValue = ref("")

function updateModelValue(e) {
    modelValue.value = e.target.value
    emit("update:modelValue", e.target.value)
}
</script>

<template>
    <input
        type="text"
        class="primary-input"
        :class="{ error : isError }"
        @input="updateModelValue"
    >
</template>

<style scoped>
.primary-input {
    height: 40px;
    width: 100%;
    color: var(--text-color);
    font-size: 16px;
    border: 1px solid var(--primary-color);
    border-radius: 15px;
    font-weight: 600;
    padding: 0 15px;
}

.primary-input:focus {
    outline: none;
    border: 2px solid var(--primary-color);
}

.primary-input::placeholder {
    color: var(--placeholder-color);
    font-size: 14px;
}

.primary-input.error {
    border: 2px solid var(--error-color);
}
</style>
