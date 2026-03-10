<template>
    <div class="goal-categories-wrapper bg-white shadow--lg">
        <div class="bg-img-full" style="background-image: url('https://i.imgur.com/cVBId2c.png');">
            <div class="container pt-game">
                <div class="tracking-wrapper">
                    <div class="sprite-card-list">
                        <div class="row gx-5">
                            <div class="col-3" v-for="(category, index) in goal_categories" :key="index">
                                <div class="sprite-card-wrapper">
                                    <div class="" @click="openCategoryDetail" :id="category.id">
                                        <div class="title text-white fs-32 text-border text-center ls-1 mb-3 f-game">
                                            {{ category.name }}</div>
                                        <div class="bg-white radius-16 py-2 px-3 info shadow--lg mb-3 category-info">
                                            <div class="level-wrapper">
                                                <TextLevelGoal :level-number="category.current_level"/>
                                                <div class="level-bar mb-3" :class="{ 
                                                    'gray-bg-2' : category.current_level == 0,
                                                    'blue-bg' : category.current_level == 1,
                                                    'green-bg' : category.current_level == 2,
                                                    'gold-bg' : category.current_level > 2,
                                                }"></div>
                                                <div
                                                    class="text-sm-bold text-warning mb-3 fs-12 d-flex align-items-center">
                                                    <div class="me-2">{{ category.production }} / {{
                                                    category.production_period }}</div>
                                                    <Image src="/coin.svg"></Image>
                                                </div>
                                                <div class="timer text-bold text-disabled fs-12 invisible">
                                                    02:23:47 till collection
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <ButtonGame class="w-100 my-5"
                                            :class="{ 'invisible' : !category.button_collect_visible }">
                                            Ready to collect<br />
                                            {{ category.production }}
                                            <Image src="/coin.svg"></Image>
                                        </ButtonGame>
                                        <Image class="img-fluid img-icon-category w-100" :src="category.icon_link">
                                        </Image>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { inject } from "vue"
import ButtonGame from "@/components/ButtonGame.vue"
import Image from "@/components/Image.vue"
import GoalContent from "@/components/RightSidebarContent/GoalContent.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import { useGoalCategoriesStore } from "@/stores/goalCategoriesStore"
import { storeToRefs } from 'pinia';

const { goal_categories } = storeToRefs(useGoalCategoriesStore());
const { fetchGoalCategories, fetchGoalCategory } = useGoalCategoriesStore();
fetchGoalCategories();

let { passRightSidebarContent, changeActivedSidebarMenu, passRightSidebarContentProps } = inject("rightPanelStatus");

let openCategoryDetail = (e) => {
    passRightSidebarContent(GoalContent);
    changeActivedSidebarMenu(0);
    fetchGoalCategory(e.currentTarget.id);
    // passRightSidebarContentProps({ 'goalCategories' : goal_categories.value[e.currentTarget.id] });
}


</script>

<style scoped>
.goal-categories-wrapper {
    height: 670px;
    position: relative;
    z-index: 9;
}

.sprite-card-wrapper .title {
    border-bottom: solid 3px transparent;
}

.sprite-card-wrapper {
    cursor: pointer;
    user-select: none;
}

.tracking-wrapper {
    position: relative;
}

.sprite-card-wrapper .card-logo {
    position: absolute;
    bottom: 0;
}

.sprite-card-wrapper:hover .title {
    text-shadow: -2px -2px 0 #294A83,
        2px -2px 0 #294A83,
        -2px 2px 0 #294A83,
        2px 2px 0 #294A83,
        0px 6px 4px rgb(0 0 0 / 25%);
    color: #2A9F60 !important;
    border-bottom: solid 3px #294A83;
}

.sprite-card-wrapper img.img-icon-category {
    max-width: 176px;
    margin: 0 auto;
    display: table;
}

.sprite-card-wrapper:hover img.img-icon-category {
    max-width: 208px;
    transition: all 0.5s;
}
.bg-img-full {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: left bottom -120px;
    background-size: cover;
    height: 100vh;
    max-height: 580px;
    position: relative;
}
.pt-game {
    padding-top: 6.75rem;
}
@media only screen and (min-width: 1400px) {
    .pt-game {
        max-width: 1140px;
    }
}
</style>