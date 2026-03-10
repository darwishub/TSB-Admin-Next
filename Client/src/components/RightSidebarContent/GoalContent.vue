<template>
    <div class="user-select-none">
        <div class="mb-3">
            <div class="text-slider">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="prev cursor-pointer" @click="prevCategoryDetail">
                        <Ionicon icon_name="chevron-back" font_size="40" class="green-2" />
                    </div>
                    <div class="text-black fs-32 ls-1 text-border-blue f-game text-green goal-category-title">
                        {{ goal_category.name }}
                    </div>
                    <div class="next cursor-pointer" @click="nextCategoryDetail">
                        <Ionicon icon_name="chevron-forward" font_size="40" class="green-2" />
                    </div>
                </div>
            </div>
        </div>
        <div class="goal-info mb-3">
            <div class="d-flex align-items-center">
                <div class="fs-16 text-sm-bold text-secondary">Production :</div>
                <div class="text-sm-bold text-warning ms-3 fs-12 d-flex align-items-center">
                    <div class="me-2">{{ goal_category.production }} / {{ goal_category.production_period }} </div>
                    <Image src="/coin.svg"></Image>
                </div>
            </div>
        </div>
        <div class="goal-status mb-c-2">
            <div class="d-flex align-items-center">
                <div class="fs-16 text-sm-bold text-secondary">Status :</div>
                <ButtonGame class="ms-3" :class="{ 'invisible' : !goal_category.button_collect_visible }">
                    Ready to collect
                </ButtonGame>
            </div>
        </div>
        <div class="level-option mb-c-2">
            <div class="d-flex justify-content-between align-items-center">
                <div class="dropdown">
                    <Ionicon icon_name="chevron-down-outline" font_size="40" class="grey-light-1" />
                </div>
                <TextLevelGoal :level-number="goal_category.current_level"></TextLevelGoal>
                <div class="level-bar" :class="{ 
                    'gray-bg-2' : goal_category.current_level == 0,
                    'blue-bg' : goal_category.current_level == 1,
                    'green-bg' : goal_category.current_level == 2,
                    'gold-bg' : goal_category.current_level > 2,
                }"></div>
            </div>
        </div>
        <div class="mx-auto d-table mb-c-2">
            <Image class="img-fluid" :src="goal_category.icon_link"></Image>
        </div>
        <div class="mb-c-2">
            <div class="fs-24 text-green text-sm-bold mb-2">What</div>
            <div class="text-sm-bold text-secondary fs-16 mb-2">{{ goal_category.what }}</div>
            <div class="text-sm-bold text-secondary fs-16">Level 1: Basic personal information of your and your team
            </div>
        </div>
        <div class="mb-c-2">
            <div class="fs-24 text-green text-sm-bold mb-2">Why</div>
            <div class="text-sm-bold text-secondary fs-16 mb-2">{{ goal_category.why }}</div>
        </div>
        <div class="d-flex align-items-center mb-4">
            <div class="fs-24 text-sm-bold me-3">Personel goals:</div>
            <TextLevelGoal :level-number="goal_category.current_level"></TextLevelGoal>
        </div>
        <SwitchCheckbox id="incomplete-status" for="incomplete-status" class="mb-6">
            Not completed
        </SwitchCheckbox>
        <!-- <Goal :is_locked="false" label="Required for meeting investor" title="This is new goal"
            logo="https://www.wikihow.com/images/thumb/d/dc/Create-a-Good-Article-Step-8.jpg/v4-460px-Create-a-Good-Article-Step-8.jpg.webp"
            :current_step="0" :total_step="5" :total_point="50" class="mx-auto mb-6" />
        <Goal :is_locked="false" label="Required for meeting investor" title="This is new goal"
            logo="https://www.wikihow.com/images/thumb/d/dc/Create-a-Good-Article-Step-8.jpg/v4-460px-Create-a-Good-Article-Step-8.jpg.webp"
            :current_step="0" :total_step="5" :total_point="50" class="mx-auto" /> -->
    </div>
</template>

<script setup>
import Ionicon from "@/components/Ionicon.vue"
import ButtonGame from "@/components/ButtonGame.vue"
import Image from "@/components/Image.vue"
import SwitchCheckbox from "@/components/SwitchCheckbox.vue"
import Goal from "@/components/Goals/Goal.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import { useGoalCategoriesStore } from "@/stores/goalCategoriesStore"
import { storeToRefs } from 'pinia';

const { goal_category  } = storeToRefs(useGoalCategoriesStore());
const { nextCategoryDetail, prevCategoryDetail } = useGoalCategoriesStore();

</script>

<style scoped>
.level-option {
    max-width: 75%;
    margin: 0 auto;
}

.goal-category-title {
    border-bottom: solid 3px #294A83;
}
</style>