<template>
    <div class="base-file-uploader">
        <label class="form-label f-game fs-20 base-grey">{{ props.label }}<span class="text-danger"
                v-if="props.isRequired">*</span></label>
        <FilePondComponent :name="name" ref="pond"
            :label-idle="`<div><img src='/upload-icon.svg' class='mx-3'>${props.textButton != undefined ? props.textButton : 'Upload file'}</div><img src='/add-icon.svg' class='mx-3 float-end'>`"
            v-bind:allow-multiple="true" :accepted-file-types="props.filesType" :max-files="props.maxData"
            maxFileSize="10MB" :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }" @addfile="onAddFile"
            @removefile="onRemoveFile" :files="initialFiles" :disabled="props.isDisabled" />
        <div v-show="errorMessage || meta.valid"
            :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }">
            {{ errorMessage || successMessage }}
        </div>
    </div>
</template>

<script setup>
import { ref, computed, toRefs, watchEffect } from "vue"
import { useField } from 'vee-validate';
import vueFilePond from "vue-filepond";
import "filepond/dist/filepond.min.css";
import "filepond-plugin-image-preview/dist/filepond-plugin-image-preview.min.css";
import FilePondPluginFileValidateType from "filepond-plugin-file-validate-type";
import FilePondPluginImagePreview from "filepond-plugin-image-preview";
import FilePondPluginFileValidateSize from 'filepond-plugin-file-validate-size';
import { storeToRefs } from 'pinia';
import { useGoalsStore } from "@/stores/goalsStore"
const { goal, goals, StepNow } = storeToRefs(useGoalsStore());
const { uploadFile, deleteFile } = useGoalsStore();

const FilePondComponent = vueFilePond(FilePondPluginFileValidateType, FilePondPluginImagePreview, FilePondPluginFileValidateSize);
const pond = ref();
const Files = ref([]);
const FilesObj = ref([]);
const initialFiles = ref([]);

const props = defineProps({
    value: {
        type: [String, Array]
    },
    label: {
        type: String,
        default: ""
    },
    name: {
        type: String,
        required: true,
        default: ""
    },
    maxData: {
        type: Number,
        default: ""
    },
    successMessage: {
        type: String,
        default: ""
    },
    placeholder: {
        type: String,
        default: ""
    },
    isRequired: {
        type: Boolean,
        default: false
    },
    isDisabled: {
        type: Boolean,
        default: false
    },
    filesType: {
        type: Array,
        default: []
    },
    textButton: {
        type: String,
        default: ""
    }
});

const name = computed(() => props.name);
const { value } = toRefs(props);

const {
    value: fileValue,
    errorMessage,
    handleBlur,
    handleChange,
    meta,
    setErrors,
} = useField(name.value, props.isRequired ? value => {
    if (!value || value.length == 0) {
        return 'this is a required field';
    }
    return true;
} : undefined
);
const filesRef = ref();
const onAddFile = async (error, file) => {
    if (!error) {
        let formData = new FormData();
        FilesObj.value.push(file.file);
        FilesObj.value.forEach((file, index) => {
            formData.append(`file[]`, file);
        });

        try {
            const result = await uploadFile(formData);
            if (result?.data?.success) {
                Files.value.push(file.filename);
                let regex = new RegExp("[^a-zA-Z0-9]", "g");
                let values = Files.value;

                values = values.map(value => {
                    if (regex.test(value)) {
                        return value.toLowerCase().replace(regex, "");
                    } else {
                        return value ? value.toLowerCase() : value;
                    }
                });

                handleChange(values);
            } else {
                setErrors([`${result?.response?.data?.message}`]);
            }
        } catch (error) {
            setErrors([error.message]);
        }
    }
}

const removeObjectFileByName = (arr, name) => {
    const objWithNameIndex = arr.findIndex((obj) => obj.name === name);
    if (objWithNameIndex > -1) {
        arr.splice(objWithNameIndex, 1);
    }
    return arr;
}

const onRemoveFile = async (error, file) => {
    if (!error) {
        const indexRemoved = Files.value.indexOf(file.filename);
        if (indexRemoved !== -1) {
            try {
                const result = await deleteFile(file.filename);
                if (result?.data?.success) {
                    Files.value.splice(indexRemoved, 1);
                    removeObjectFileByName(FilesObj.value, file.filename);
                    let regex = new RegExp("[^a-zA-Z0-9]", "g");
                    let values = Files.value;

                    values = values.map(value => {
                        if (regex.test(value)) {
                            return value.toLowerCase().replace(regex, "");
                        } else {
                            return value ? value.toLowerCase() : value;
                        }
                    });

                    handleChange(values);
                } else {
                    setErrors([`${result?.response?.data?.message}`]);
                }
            } catch (error) {
                setErrors([error.message]);
            }
        }
    }
}

watchEffect(() => {
    try {
        const { value: val } = value;
        if (val) {
            const items = Array.isArray(val) ? val : [val];
            items.forEach((item) => {
                if (item) {
                    const cleanItem = item.replace(/[^a-zA-Z0-9]+/g, '');
                    initialFiles.value.push(`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${cleanItem}`);
                }
            });
        }
    } catch (error) {
        console.error(error);
        // Handle the error appropriately here
        // For example, you might want to show an error message to the user, or log the error to an error tracking service
    }
}, { flush: 'post' });


defineExpose({
    FilesObj
});

</script>

<style scoped></style>