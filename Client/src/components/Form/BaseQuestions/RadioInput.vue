<template>
<div>
    <BaseInput :name="`${name}.schema.props.label`" label="Question" placeholder="Enter question title here" type="text"
        type-rule="text" class="mb-3" isRequired />
    <BaseInput :name="`${name}.schema.props.placeholder`" label="Question placeholder" placeholder="Enter placeholder here"
        type="text" type-rule="text" class="mb-3" isRequired />
    <BaseInputSwitch :name="`${name}.schema.props.required`" label="Required" />
    <BaseInputSwitch :name="`${name}.schema.props.type`" label="Allow Multiple Answers" 
    true-value="checkbox" false-value="radio"
    />
    <BaseTextareaAutosize label="Add More Dropdown Choices" :name="`${name}.schema.option`" text-button="Add dropdown choice" is-required />
</div>
</template>

<script setup>
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseDatePicker from "@/components/Form/BaseDatePicker.vue"
import BaseTextarea from "@/components/Form/BaseTextarea.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import BaseInputCheckbox from "@/components/Form/BaseInputCheckbox.vue"
import BasePickSlider from "@/components/Form/BasePickSlider.vue"
import BaseFileUploader from "@/components/Form/BaseFileUploader.vue"
import BaseTextareaAutosize from "@/components/Form/BaseTextareaAutosize.vue"
import { computed, ref, watch, toRefs } from "vue"

const props = defineProps({
    name: {
        type: String,
    },
});

const { name } = toRefs(props);

</script>

<style scoped>
</style>