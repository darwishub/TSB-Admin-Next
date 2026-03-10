<template>
    <div class="next-features shadow--lg py-5 position-relative">
        <div class="container">
            <div class="row justify-content-between" :class="[isArrowOpen ? 'arrow-unfold' : 'arrow-fold' ]">
                <div class="col-1">
                    <Ionicon icon_name="chevron-up-circle" class="toggle-transition disabled invisible"
                        font_size="32" />
                </div>
                <div class="col-3">
                    <div class="ts" :class="{ 'mt-6' : isArrowOpen }">
                        <div class="title text-white fs-32 text-border text-center ls-1 mb-5 f-game text-center">
                            Network
                        </div>
                        <Transition name="fade" mode="out-in">
                            <div v-show="isArrowOpen">
                                <div class="grey text-sm-bold fs-14 mb-5">Now, for interaction-heavy pages, smaller text
                                    sizes
                                    are perfectly acceptable. In fact, depending on the amount of data your user is
                                    taking
                                    in at
                                    once, even 18px text is uncomfortably
                                </div>
                            </div>

                        </Transition>
                        <Image class="mx-auto d-table" src="/network-icon.png" />
                    </div>
                </div>
                <div class="col-4">
                    <Image class="mx-auto d-table ts" src="/arrow-more-up.svg"
                        :class="{ 'arrow-bg' : !isArrowOpen }" />
                </div>
                <div class="col-3">
                    <div class="ts" :class="{ 'mt-6' : isArrowOpen }">
                        <div class="title text-white fs-32 text-border text-center ls-1 mb-5 f-game text-center">
                            Leaderboards
                        </div>
                        <Transition name="fade" mode="out-in">
                            <div v-show="isArrowOpen">
                                <div class="grey text-sm-bold fs-14 mb-5">Now, for interaction-heavy pages, smaller text
                                    sizes
                                    are perfectly acceptable. In fact, depending on the amount of data your user is
                                    taking
                                    in at
                                    once, even 18px text is uncomfortably
                                </div>
                            </div>

                        </Transition>
                        <Image class="mx-auto d-table" src="/leaderboard-icon.png" />
                    </div>
                </div>
                <div class="col-1 cursor-pointer" @click="arrowToggle">
                    <Ionicon icon_name="chevron-up-circle" class="toggle-transition disabled float-end" font_size="32"
                        :class="isArrowOpen ? 'toggle-up' : 'toggle-down'" />
                </div>
            </div>
            <div class="bg-white shadow--lg arrow-lock-status py-2 radius-8">
                <div class="mx-auto d-table text-center">
                    <Ionicon icon_name="lock-closed-outline" font_size="24" class="warning mb-2" />
                    <div class="grey fs-14 mb-2">Requires TEAM</div>
                    <div class="f-game fs-24 grey lh-c-1">LVL 2</div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import Ionicon from "@/components/Ionicon.vue";
import Image from "@/components/Image.vue"
import { ref } from "vue"

const isArrowOpen = ref(true);
const arrowToggle = () => {
    isArrowOpen.value = !isArrowOpen.value;
}

</script>

<style scope>
.next-features .arrow-lock-status {
    width: 196px;
    position: absolute;
    top: -3.5rem;
    z-index: 99;
    left: 50%;
    transform: translate(-50%, 0);
}
.fade-enter-active,
.fade-leave-active {
  transition: all 0.25s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
.next-features .ts {
    transition: all 0.5s ease;
}
.arrow-fold .arrow-bg {
    position: relative;
    z-index: 1;
    margin-top: -14.5rem;
}
</style>