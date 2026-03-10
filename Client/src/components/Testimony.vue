<template>
    <div class="testimony">
        <div class="testimony__words fs-12 mb-2">
            {{ props.comment }}
        </div>
        <div class="testimony__profile">
            <div class="d-flex align-items-center">
                <div class="flex-shrink-0">
                    <Image class="testimony__profile__img rounded-lg d-block shadow--lg" height="48" width="48"
                        :src="props.photo_url" />
                </div>
                <div class="flex-grow-1 ms-3">
                    <span class="testimony__profile__name text-sm-bold fs-16 d-block">{{
                            props.name
                    }}</span>
                    <small class="testimony__profile__title fs-12 d-block">{{
                            props.company
                    }}</small>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>

import Image from "@/components/Image.vue";

const props = defineProps({
    comment: {
        type: String
    },
    photo_url: {
        type: String
    },
    name: {
        type: String
    },
    company: {
        type: String
    }
});

</script>

<style scoped>
.slide-fade-enter-active {
    -webkit-transition: all 0.5s ease-out;
    -o-transition: all 0.5s ease-out;
    transition: all 0.5s ease-out;
}

.slide-fade-leave-active {
    -webkit-transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
    -o-transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
    transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}

.slide-fade-enter-from,
.slide-fade-leave-to {
    -webkit-transform: translateX(20px);
    -ms-transform: translateX(20px);
    transform: translateX(20px);
    opacity: 0;
}

.testimony__profile__name {
    height: 25px;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}

.testimony__profile__title {
    max-width: 100%;
    overflow: hidden;
    word-break: break-word;
    text-overflow: ellipsis;
    height: 1.25rem;
}
</style>