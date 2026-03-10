<template>
<div>
    <BaseInput :name="`${name}.schema.props.label`" label="Question" placeholder="Enter question title here" type="text"
        type-rule="text" class="mb-3" isRequired />
        <BaseInput :name="`${name}.schema.option[0]`" label="Choice #1 Title (Positive)" placeholder="Enter choice #1" type="text"
        type-rule="text" class="mb-3" isRequired />
    <BaseInput :name="`${name}.schema.option[1]`" label="Choice #2 Title (Negative)" placeholder="Enter choice #2"
        type="text" type-rule="text" class="mb-3"  isRequired/>
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

</script>

<style scoped></style>