<template>
<div>
    <BaseSelect :name="`${name}.schema.props.type_rule`" label="Choose Field Type" placeholder="Select on these options"
        class="mb-3" isRequired :options="options" 
        value-by="value" label-by="label" />
    <BaseInput :name="`${name}.schema.props.label`" label="Question" placeholder="Enter question title here" type="text"
        type-rule="text" class="mb-3" isRequired />
    <BaseInput :name="`${name}.schema.props.placeholder`" label="Question placeholder" placeholder="Enter placeholder here"
        type="text" type-rule="text" class="mb-3" isRequired />
    <BaseInputSwitch :name="`${name}.schema.props.required`" label="Required" />
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
const options = [{ label: "Text", value: "text" }, { label: "Linkedin", value: "linkedin" }, { label: "Email", value: "email" }, { label: "URL", value: "url" }, { label: "Currency (USD)", value: "money" }];
</script>

<style scoped>
</style>