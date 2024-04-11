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
    <textarea
        class="primary-textarea"
        :class="{ error : isError }"
        @input="updateModelValue"
    ></textarea>
</template>

<style scoped>
.primary-textarea {
    height: 400px;
    width: 100%;
    color: var(--text-color);
    font-size: 16px;
    border: 1px solid var(--primary-color);
    border-radius: 15px;
    font-weight: 600;
    padding: 15px;
    resize: none;
}

@media (max-width: 600px) {
    .primary-textarea {
        height: 300px;
    }
}

.primary-input:focus {
    outline: none;
    border: 2px solid var(--primary-color);
}

.primary-textarea::placeholder {
    color: var(--placeholder-color);
    font-size: 14px;
}

.primary-textarea.error {
    border: 2px solid var(--error-color);
}
</style>
