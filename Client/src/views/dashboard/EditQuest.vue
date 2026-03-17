<template>
    <PageExplanation :logo="adminAuth?.photo" :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" >
        <template v-slot:top-content>
            <div class="d-flex justify-content-between align-items-center">
                <div class="d-flex justify-content-start align-items-center">

                    <!-- <Button class="blue rounded-md p-1 ps-3 pe-3 me-3" type="button"
                        :disabled="buttonPublishStatus.loading && isClicked == 1" @click="saveChanges"
                        v-if="metaForm.dirty">

                        <div class="d-flex justify-content-center align-items-center">
                            <div class="f-game fs-24">{{ textSave }}</div>
                            <Ionicon icon_name="save" class="ms-1" font_size="20" />
                        </div>
                    </Button> -->
                    <span class="d-inline-block">
                        <Button class="grey rounded-md p-1 ps-3 pe-3 me-3" type="button" @click="onPreview">
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="f-game fs-24">Preview</div>
                                <Ionicon icon_name="eye-outline" class="ms-1" font_size="20" />
                            </div>
                        </Button>
                    </span>

                </div>
                <div class="f-game fs-24 text-info">{{ textSave }}</div>
                <BaseTextarea name="goal_program_id" type="text" class="d-none" label="helper"
                    :value="currentProgramId" />
                <div class="d-flex justify-content-end align-items-center">
                    <span :data-bs-toggle="isEveryStepHasQuestions ? '' : 'tooltip'" data-bs-placement="bottom"
                        data-bs-title="Each step must have at least a question." tabindex="0" class="d-inline-block"
                        v-if="!goal?.is_published">
                        <Button class="blue rounded-md p-1 ps-3 pe-2" type="button"
                            :loading="buttonPublishStatus.loading && isClicked == 2"
                            :is_success="buttonPublishStatus.success && isClicked == 2"
                            :is_fail="buttonPublishStatus.fail && isClicked == 2"
                            v-on="isEveryStepHasQuestions ? { click: onPublish } : {}"
                            :disabled="!isEveryStepHasQuestions || buttonPublishStatus.disabled">
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="f-game fs-24">Publish</div>
                                <Ionicon icon_name="chevron-forward-outline" class="ms-1" font_size="24" />
                            </div>
                        </Button>

                    </span>
                    <span tabindex="0" class="d-inline-block" v-else>
                        <Button class="red rounded-md p-1 ps-3 pe-2" type="button"
                            :loading="buttonPublishStatus.loading && isClicked == 2"
                            :is_success="buttonPublishStatus.success && isClicked == 2"
                            :is_fail="buttonPublishStatus.fail && isClicked == 2"
                            v-on="isEveryStepHasQuestions ? { click: onUnpublish } : {}"
                            :disabled="((!isEveryStepHasQuestions) || buttonPublishStatus.disabled)">
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="f-game fs-24">Unpublish</div>
                                <Ionicon icon_name="chevron-forward-outline" class="ms-1" font_size="24" />
                            </div>
                        </Button>
                    </span>
                </div>

            </div>
        </template>
        <template v-slot:body-content>

            <div class="mt-3 mb-5">
                <div class="stepper-wrapper align-items-start">

                    <div class="stepper-item cursor-pointer user-select-none" v-for="(step, index) in steps"
                        :key="index" :class="step.value.is_step_complete ? 'completed' : ''">
                        <div class="f-game fs-24 lh-1 mb-3 invisible">Reward</div>
                        <div class="px-3 py-2 mb-2">
                            <div @click="viewStep(index)">
                                <div class="text-sm-bold shadow--lg rounded-lg step-counter d-flex justify-content-center align-items-center"
                                    :class="[step.value.is_step_complete && currentStepRef != index ? 'orange-bg-light-2 border-orange text-warning' : '',
                        currentStepRef == index ? 'blue-bg text-white' : 'gray-bg-2 text-white'
                        ]" style="height: 48px; width: 48px;">

                                    <div class="text-number" v-if="!step.value.is_step_complete">
                                        <Ionicon icon_name="add-outline" font_size="24" class="white" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="text-center">
                            <div class="fs-12 text-sm-bold mb-2 text-center step-title-sm">{{ step.value.step_title }}
                            </div>
                            <div class="fs-12 text-sm-bold grey">Step {{
                            index
                        }}/5</div>
                        </div>

                    </div>

                    <div class="stepper-item cursor-pointer" v-show="(currentStep < 5)">
                        <div class="f-game fs-24 lh-1 mb-3 invisible">Reward</div>
                        <div class="px-3 py-2 mb-2">
                            <div class="text-sm-bold shadow--lg rounded-lg step-counter gray-bg-2 text-white d-flex justify-content-center align-items-center"
                                style="height: 48px; width: 48px;">

                            </div>
                        </div>
                        <div class="fs-20 text-md mb-1 text-center f-game text-green"></div>
                    </div>

                    <div class="stepper-item">
                        <div class="f-game fs-24 lh-1 mb-3 text-warning">Reward</div>
                        <div
                            class="goal-reward-wrapper gray-bg-2 radius-8 shadow--sm py-2 px-3 step-counter cursor-pointer user-select-none">
                            <div class="d-flex align-items-center">
                                <div class="flex-shrink-0">
                                    <Image src="/reward.png" class="rounded-lg" height="48" width="48" />
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <div class="f-game fs-24 lh-1 text-white">Earn
                                        <Image src="/coin-2.svg" />
                                    </div>
                                    <div class="text-sm-bold fs-14 text-white">{{ goalReward }} TSB coins</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </template>
    </PageExplanation>

    <form>
        <div class="goal-details-hero py-6 shadow--lg mb-6"
            :style="[imgUrl != null ? { 'background-image': 'url(' + imgUrl + ')' } : { 'background-color': '#c2c2c2' }]">
            <div class="container">
                <div class="position-relative">
                    <div class="d-flex align-items-center add-goal-text bg-white py-1 px-3 radius-8 shadow-out--sm"
                        @click="addGoalImage" id="goalimg">
                        <div class="flex-shrink-0">
                            <Image src="/img-upload.svg" width="24" height="24" />
                        </div>
                        <div class="flex-grow-1">
                            <div class="f-game fs-20 grey ms-3">{{ `${imgUrl != null ? 'Change' : 'Upload'} Picture Max
                                1
                                MB` }}
                            </div>
                            <div v-show="errorMessage || meta.valid" class="text-danger text-bold fs-14 ms-3">
                                {{ errorMessage }}
                            </div>
                        </div>
                    </div>
                    <input class="goal-img-input" type="file" @blur="handleBlur" name="goal_img"
                        accept="image/png, image/jpeg, image/jpg" @change="onGoalImageChange" ref="goalImageRef">
                    <input type="hidden" name="file_url">

                </div>

                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <button class="nav-link active" id="nav-home-tab" data-bs-toggle="tab"
                            data-bs-target="#nav-home" type="button" role="tab" aria-controls="nav-home"
                            aria-selected="true">Quest Card</button>
                    </div>
                </nav>
                <div class="tab-content shadow-out--sm" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab"
                        tabindex="0">
                        <div class="row justify-content-between">
                            <template v-if="goal">
                                <div class="col-3">
                                    <div class="base-grey f-game fs-20">Choose level and category</div>

                                    <BaseSelect :options="levelIdOptions" name="level_code_number" type-rule="number"
                                        placeholder="Select level" class="custom-select-option" :is-required="true"
                                        @onSelect="onSelectLevel" :value="selectLevel">

                                        <template #selected="{ selected }">
                                            <template v-if="selected">
                                                <div class="d-flex justify-content-start align-items-center">
                                                    <TextLevelGoal :levelNumber="selected" />
                                                    <LevelBar class="ms-3" :levelNumber="selected" />
                                                </div>
                                            </template>
                                            <template v-else>
                                                <div class="fs-14 base-grey">Select a quest level</div>
                                            </template>
                                        </template>

                                        <template #option="{ option }">
                                            <div class="d-flex justify-content-start align-items-center">
                                                <TextLevelGoal :levelNumber="option" />
                                                <LevelBar class="ms-3" :levelNumber="option" />
                                            </div>
                                        </template>

                                    </BaseSelect>

                                    <BaseSelect v-if="categoryOptions" :options="categoryOptions" name="level_id"
                                        placeholder="Select level" class="mb-3 custom-select-option" :is-required="true"
                                        value-by="id" :empty-model-value="''">

                                        <template #selected="{ selected }">
                                            <template v-if="selected">
                                                <div class="d-flex justify-content-start align-items-center">
                                                    <Image class="of-contain" :src="selected.logo_link" width="96"
                                                        height="96" />
                                                    <div class="fs-24 base-grey ms-3 f-game">{{ selected.name }}</div>
                                                </div>
                                            </template>
                                            <template v-else>
                                                <div class="fs-14 base-grey">Select a quest category</div>
                                            </template>
                                        </template>

                                        <template #option="{ option }">
                                            <div class="d-flex justify-content-start align-items-center">
                                                <Image class="of-contain" :src="option.logo_link" width="45"
                                                    height="45" />
                                                <div class="fs-20 base-grey ms-3 f-game">{{ option.name }}</div>
                                            </div>
                                        </template>

                                    </BaseSelect>
                                    <BaseSelect :options="programGroupList" name="program_group_id" label="Program Group"
                                        class="mt-3 custom-select-option"
                                        v-if="programGroup.length > 0" 
                                        value-by="programGroupId"
                                        label-by="groupName"
                                        placeholder="Select a group"
                                    />
                                </div>
                                <div class="col-6">
                                    <BaseInput name="goal_id" type="hidden" class="d-none" />
                                    <BaseInput name="goal_title" label="Goal Title" class="mb-3"
                                        placeholder="Example: Introduce your team" is-required />
                                    <BaseInput name="goal_subtitle" label="Goal Subtitle" class="mb-3"
                                        placeholder="Example: Introduce your team" />
                                    <BaseTextarea name="goal_description" label="Goal Description" :is-required="true"
                                        class="mb-3" placeholder="Describe your team in few paragraphs" />
                                    <BaseTextarea name="goal_help_text" label="Goal Help Text" class="mb-3"
                                        placeholder="Tips to completing this goal" />
                                </div>
                                <div class="col-3">
                                    <div class="text-warning f-game fs-24 mb-3 lh-1">Reward:</div>
                                    <BaseInput name="goal_reward" label="Edit Reward" type-rule="coins" class="mb-3"
                                        placeholder="0" is-disabled :value="goalReward" ref="rewardInputRef" />
                                    <BaseTags label="Edit Tags" name="goal_label" placeholder="Press enter to add a tag"
                                        class="mb-3" />
                                    <BaseInputSwitch name="status_score" label="Enable scoring" class="mt-3"
                                        @onSwitchChange="enableScoring" />
                                </div>
                            </template>
                        </div>
                        <div class="row justify-content-center">
                            <div class="col-4">

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container" id="form-section">
            <div class="mb-6">
                <div class="fs-24 text-sm-bold mb-3">Step : <span class="grey ms-1"> {{ currentStepRef }}/5</span></div>
                <div class="fs-14 grey text-sm-bold">Follow the steps and fill in the fields bellow to create quests for
                    your
                    users that they can fill in and get rewards. If you need any help check the FAQ section.</div>
            </div>
            <div class="row mb-8 gx-5">
                <div class="col-4">
                    <div class="bg-white p-3 body-content radius-16 shadow-out--sm sticky-step-left sticky-top">
                        <div class="text-sm-bold fs-14 mb-3">Sidebar showing steps :</div>
                        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none" :class="[index < steps.length ? 'mb-3' : '',
                        currentStepRef == index ? 'current' : '',
                        !step.isLast ? 'complete' : '']" v-for="(step, index) in steps" :key="step.key" id="tab"
                            @click="viewStep(index)">

                            <div class="flex-shrink-0">
                                <div class="step-number text-sm-bold shadow--lg">
                                    <div class="step-number-text">
                                        {{ index }}
                                    </div>
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <BaseInput :name="`steps[${index}].step`" type="hidden" class="d-none"
                                    :value="step.value.step" />
                                <div class="me-auto">
                                    <div class="text-sm-bold fs-14 step-title">{{ step.value.step_title }}</div>
                                    <span class="text-sm-bold grey fs-14 step-info">Step
                                        {{
                            index
                        }}/5</span>
                                </div>
                            </div>
                            <div class="flex-grow-2">
                                <!-- <Ionicon icon_name="chevron-forward" class="grey-light-1" font_size="32" /> -->

                                <Button class="red rounded-sm p-2 ms-2" type="button" ref="deleteStepRef"
                                    v-show="index > 0" :loading="deleteStepLoading && deleteStepId == index"
                                    :disabled="deleteStepLoading" @click="DeleteStep(step.value.step, index)"
                                    id="btn-delete">
                                    <Ionicon icon_name="trash" font_size="24" />
                                </Button>
                            </div>
                        </div>

                        <div class="d-flex align-items-center py-2 px-3 cursor-pointer user-select-none mt-3">
                            <div class="flex-shrink-0">
                                <Image src="/reward.png" class="rounded-lg" width="48" height="48" />
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <div class="me-auto">
                                    <div class="text-sm-bold fs-14 text-warning">Reward</div>
                                    <span class="text-sm-bold fs-14 text-warning">{{ goalReward }}</span>
                                </div>
                            </div>
                            <div class="flex-grow-2">
                                <Image src="/coin-2.svg" height="24" width="24"></Image>
                            </div>
                        </div>
                        <!-- <pre>{{values}} </pre> -->
                    </div>

                </div>
                <div class="col-8">
                    <div class="goal-tasks-wrapper radius-16 shadow-out--sm"
                        :style="[imgUrl != null ? { 'background-image': 'url(' + imgUrl + ')' } : { 'background-color': '#c2c2c2' }]">

                        <template v-for="(field, index) in steps" :key="index">
                            <Presence>
                                <Motion v-show="(index == currentStepRef)" :initial="false"
                                    :animate="{ opacity: 1, x: 0, transition: { duration: 0.8, delay: 0.25 } }"
                                    :exit="{ opacity: 0, x: 50, transition: { duration: 0, delay: 0 } }">
                                    <GoalCardWrapper title="Sidebar Info" class="mb-5">
                                        <BaseInput :name="`steps[${index}].step_title`" label="Sidebar Step Title"
                                            :is-required="true" placeholder="Enter sidebar step title here."
                                            class="mb-3" />
                                    </GoalCardWrapper>
                                </Motion>
                            </Presence>

                        </template>

                        <Presence>
                            <Motion v-show="currentStepRef == 0" :initial="false"
                                :animate="{ opacity: 1, x: 0, transition: { duration: 0.8, delay: 0.25 } }"
                                :exit="{ opacity: 0, x: 50, transition: { duration: 0, delay: 0 } }">
                                <GoalCardWrapper title="Goal Introduction">
                                    <BaseInput :name="`steps[0].cards[0].schema.field_code`" type="hidden"
                                        class="d-none" :value="0" />
                                    <BaseInput :name="`steps[0].cards[0].title`" :is-required="true" label="Card Title"
                                        placeholder="Enter card title here." class="mb-3" />
                                    <BaseTextarea :name="`steps[0].cards[0].description`" label="Card Description"
                                        :is-required="true"
                                        placeholder="Example : Describe your team in few paragraphs" />
                                </GoalCardWrapper>
                            </Motion>
                        </Presence>

                        <FieldArray :name="`steps[${currentStepRef}].cards`"
                            v-slot="{ fields: cardFields, push, remove, update, insert }">

                            <template v-if="currentStep > 0">

                                <template v-for="(step, stepIndex) in stepsData" :key="step.key">

                                    <Presence v-if="step.value?.cards?.length == 0 || step.value?.cards == undefined">
                                        <Motion :initial="false"
                                            :animate="{ opacity: 1, x: 0, transition: { duration: 0.8, delay: 0.25 } }"
                                            :exit="{ opacity: 0, x: 50, transition: { duration: 0, delay: 0 } }"
                                            v-show="currentStepRef - 1 == stepIndex && existingFieldsIndex == undefined">
                                            <GoalCardWrapper :title="`Add field ${1} / 3`"
                                                class="goal-builder-active mb-5" :class="{
                            'pe-none': updateLoading || pickFieldLoading,
                        }">

                                                <div class="">

                                                    <Button class="white rounded-md py-2 px-5 d-table mx-auto"
                                                        type="button" @click="showExistingFields(0)">
                                                        <div class="d-flex justify-content-center align-items-center">
                                                            <div class="f-game fs-24">Use existing fields</div>
                                                            <Ionicon icon_name="copy-outline" class="ms-1"
                                                                font_size="20" />
                                                        </div>
                                                    </Button>

                                                    <p class="text-sm-bold fs-20 text-center my-4">or create your own
                                                    </p>
                                                </div>

                                                <div class="row">
                                                    <div class="col-4 mb-4" v-for="(btn, btnIndex) in goalBuilderObj"
                                                        :key="btnIndex">
                                                        <div class="bg-white shadow-out--sm radius-16 cursor-pointer user-select-none py-3"
                                                            @click="pickField(btnIndex, step.value.step, btn.field_code)">

                                                            <div class="d-flex flex-column align-items-center">
                                                                <Image :src="btn.icon_url" height="24" width="24" />
                                                                <div class="base-grey f-game fs-20 text-md">{{
                            pickFieldLoading && pickFieldSelectedId == btnIndex
                                ? "Loading..." : btn.btn_name
                        }}</div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </GoalCardWrapper>
                                        </Motion>
                                    </Presence>

                                    <Presence>
                                        <Motion :initial="false"
                                            :animate="{ opacity: 1, x: 0, transition: { duration: 0.8, delay: 0.25 } }"
                                            :exit="{ opacity: 0, x: 50, transition: { duration: 0, delay: 0 } }"
                                            v-show="existingFieldsIndex != undefined"
                                            v-if="step.value?.cards?.length == 0 || step.value?.cards == undefined">
                                            >
                                            <GoalCardWrapper title="Use existing fields" class="mb-5">

                                                <div class="row mb-5">
                                                    <BaseSelect :options="[12, 24]" name="show_data" label="Show :"
                                                        :value="12" ref="showEntries" @onSelect="onSelectShowEntries"
                                                        class="col-2" style="" />
                                                    <BaseInput label="Search fields" class="col-10" name="search"
                                                        ref="searchInput" placeholder="Enter fields title here..."
                                                        @input="onInputSearch" />
                                                </div>


                                                <div class="col-12">
                                                    <div class="row g-4">
                                                        <div class="col-4" v-for="(question, index) in questionList">
                                                            <div class="existing-cards cursor-pointer"
                                                                @click="onClickExistingField(index, question)">
                                                                <div class="card p-0 shadow-out--sm radius-16 border-0"
                                                                    :class="{
                            'shadow-save': existingFieldActiveIndex == index
                        }">
                                                                    <img :src="goalBuilderObj.find(i => i.field_code === question.FieldCode)?.icon_url"
                                                                        class="card-img-top of-scale-down gray-bg-2">
                                                                    <div class="card-body">
                                                                        <small class="fs-12 text-info">{{
                            goalBuilderObj.find(i => i.field_code ===
                                question.FieldCode)?.btn_name }}</small>
                                                                        <h5
                                                                            class="card-title f-game base-grey fs-20 mb-0">
                                                                            {{ question.Label }}</h5>
                                                                        <!-- <p class="card-text base-grey fs-14">Some quick example text to build on the card title.</p> -->
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="d-flex justify-content-center mt-5">
                                                    <div>
                                                        <VueAwesomePaginate :total-items="totalPage"
                                                            :items-per-page="itemShowPerPage" :max-pages-shown="5"
                                                            v-model="currentPage" :on-click="onClickHandler" />
                                                    </div>
                                                </div>

                                                <div class="d-flex justify-content-center align-items-center mt-4">
                                                    <Button class="blue outline rounded-md p-1 px-5 mx-2" type="button"
                                                        @click="cancelExistingFields">
                                                        <div class="d-flex justify-content-center align-items-center">
                                                            <div class="f-game fs-24">Cancel</div>
                                                        </div>
                                                    </Button>

                                                    <Button class="blue rounded-md p-1 px-5 mx-2" type="button"
                                                        :loading="loadingCopy" :disabled="loadingCopy"
                                                        @click="copyField(steps[currentStepRef]?.value.step)">
                                                        <div class="d-flex justify-content-center align-items-center">
                                                            <div class="f-game fs-24">Choose</div>
                                                        </div>
                                                    </Button>

                                                </div>

                                            </GoalCardWrapper>
                                        </Motion>
                                    </Presence>

                                    <template v-for="(card, cardIndex) in step.value?.cards" :key="step.key">

                                        <Presence>
                                            <Motion v-if="Object.keys(card).length > 0" :initial="false"
                                                :animate="{ opacity: 1, x: 0, transition: { duration: 0.8, delay: 0.25 } }"
                                                :exit="{ opacity: 0, x: 50, transition: { duration: 0, delay: 0 } }"
                                                v-show="currentStepRef > 0 && currentStepRef - 1 == stepIndex">

                                                <div class="radius-16 bg-white p-5 shadow-out--sm mb-5">

                                                    <div
                                                        class="d-flex justify-content-center aling-items-center position-relative">

                                                        <div class="f-game fs-24 text-center mb-5">{{
                            `${goalBuilderObj.find(goal => goal?.field_code ==
                                card?.schema?.field_code)?.btn_name} ${cardIndex + 1} / 3`
                        }}
                                                        </div>

                                                        <BaseInput
                                                            :name="`steps[${stepIndex + 1}].cards[${cardIndex}].schema.field_code`"
                                                            type="hidden" class="d-none" />
                                                        <BaseInput v-if="card.schema.field_code != 7"
                                                            :name="`steps[${stepIndex + 1}].cards[${cardIndex}].schema.field_code_type`"
                                                            type="hidden" class="d-none" />

                                                        <div class="field-menu-control">
                                                            <ul class="list-inline">

                                                                <li class="list-inline-item">
                                                                    <Image
                                                                        src="/goal-builder-icon/show_goal_builder.svg"
                                                                        height="24" width="24"
                                                                        class="me-2 cursor-pointer d-none"
                                                                        @click="showUpdatePickedField(cardIndex)" />
                                                                    <Image src="/goal-builder-icon/delete.svg"
                                                                        height="24" width="24" class="cursor-pointer"
                                                                        @click="[
                            deletePickedField(card.id)
                        ]" />
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div>
                                                        <component :is="dynamicFields[card.schema.field_code]" v-bind="{

                            name: `steps[${stepIndex + 1}].cards[${cardIndex}]`,
                            textButton: card.schema.props.text_button,
                            fieldCodeType: card.schema.field_code_type,
                            typeRule: card.schema.props.type_rule,
                            allowAll: card.schema.props.allow_multiple

                        }" />
                                                    </div>
                                                    <BaseInput
                                                        :name="`steps[${stepIndex + 1}].cards[${cardIndex}].schema.props.score`"
                                                        label="Score" class="mt-3" placeholder="Enter score here."
                                                        type="text" type-rule="number"
                                                        v-if="values.level_code_number > 3 && values.status_score" />
                                                </div>

                                            </Motion>
                                        </Presence>



                                        <Presence>

                                            <Motion :initial="false"
                                                :animate="{ opacity: 1, x: 0, transition: { duration: 0.8, delay: 0.25 } }"
                                                :exit="{ opacity: 0, x: 50, transition: { duration: 0, delay: 0 } }"
                                                v-show="step.value?.cards?.length < 3 && currentStep != 0 && currentStepRef - 1 == stepIndex"
                                                v-if="step.value?.cards?.length > 0">

                                                <GoalCardWrapper
                                                    :title="`${updateFieldIndex == cardIndex ? `Change` : `Add`} field ${step.value?.cards?.length + 1} / 3`"
                                                    class="goal-builder-active mb-5" :class="{
                            'pe-none': updateLoading || pickFieldLoading,
                        }"
                                                    v-show="cardIndex == step.value?.cards?.length - 1 && existingFieldsIndex != cardIndex">
                                                    <div class="row">

                                                        <div class="">

                                                            <Button class="white rounded-md py-2 px-5 d-table mx-auto"
                                                                type="button" @click="showExistingFields(cardIndex)">
                                                                <div
                                                                    class="d-flex justify-content-center align-items-center">
                                                                    <div class="f-game fs-24">Use existing fields</div>
                                                                    <Ionicon icon_name="copy-outline" class="ms-1"
                                                                        font_size="20" />
                                                                </div>
                                                            </Button>

                                                            <p class="text-sm-bold fs-20 text-center my-4">or create
                                                                your
                                                                own</p>
                                                        </div>

                                                        <div class="col-4 mb-4"
                                                            v-for="(btn, btnIndex) in goalBuilderObj" :key="btnIndex">
                                                            <div class="bg-white shadow-out--sm radius-16 cursor-pointer user-select-none py-3"
                                                                @click="pickField(btnIndex, step.value.step, btn.field_code)">

                                                                <div class="d-flex flex-column align-items-center">
                                                                    <Image :src="btn.icon_url" height="24" width="24" />
                                                                    <div class="base-grey f-game fs-20 text-md">{{
                            pickFieldLoading && pickFieldSelectedId ==
                                btnIndex
                                ? "Loading..." : btn.btn_name
                        }}</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <Button class="blue mt-6 rounded-md mx-auto d-table p-1 ps-2 pe-3"
                                                        type="button" v-if="updateFieldIndex == cardIndex"
                                                        @click="cancelUpdatePickedField">
                                                        <div class="d-flex justify-content-center align-items-center">
                                                            <Ionicon icon_name="chevron-back-outline" class="me-1"
                                                                font_size="24" />
                                                            <div class="f-game fs-24">Cancel</div>
                                                        </div>
                                                    </Button>
                                                </GoalCardWrapper>
                                            </Motion>
                                        </Presence>

                                        <Presence>
                                            <Motion :initial="false"
                                                :animate="{ opacity: 1, x: 0, transition: { duration: 0.8, delay: 0.25 } }"
                                                :exit="{ opacity: 0, x: 50, transition: { duration: 0, delay: 0 } }"
                                                v-show="existingFieldsIndex != undefined && existingFieldsIndex == cardIndex && currentStepRef != 0 && currentStepRef - 1 == stepIndex">
                                                <GoalCardWrapper title="Use existing fields" class="mb-5">

                                                    <div class="row mb-5">
                                                        <BaseSelect :options="[12, 24]" name="show_data" label="Show :"
                                                            :value="12" ref="showEntries"
                                                            @onSelect="onSelectShowEntries" class="col-2" style="" />
                                                        <BaseInput label="Search fields" class="col-10" name="search"
                                                            ref="searchInput" placeholder="Enter fields title here..."
                                                            @input="onInputSearch" />
                                                    </div>


                                                    <div class="col-12">
                                                        <div class="row g-4">
                                                            <div class="col-4"
                                                                v-for="(question, index) in questionList">
                                                                <div class="existing-cards cursor-pointer"
                                                                    @click="onClickExistingField(index, question)">
                                                                    <div class="card p-0 shadow-out--sm radius-16 border-0"
                                                                        :class="{
                            'shadow-save': existingFieldActiveIndex == index
                        }">
                                                                        <img :src="goalBuilderObj.find(i => i.field_code === question.FieldCode)?.icon_url"
                                                                            class="card-img-top of-scale-down gray-bg-2">
                                                                        <div class="card-body">
                                                                            <small class="fs-12 text-info">{{
                            goalBuilderObj.find(i => i.field_code
                                ===
                                question.FieldCode)?.btn_name }}</small>
                                                                            <h5
                                                                                class="card-title f-game base-grey fs-20 mb-0">
                                                                                {{ question.Label }}</h5>
                                                                            <!-- <p class="card-text base-grey fs-14">Some quick example text to build on the card title.</p> -->
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="d-flex justify-content-center mt-5">
                                                        <div>
                                                            <VueAwesomePaginate :total-items="totalPage"
                                                                :items-per-page="itemShowPerPage" :max-pages-shown="5"
                                                                v-model="currentPage" :on-click="onClickHandler" />
                                                        </div>
                                                    </div>

                                                    <div class="d-flex justify-content-center align-items-center mt-4">
                                                        <Button class="blue outline rounded-md p-1 px-5 mx-2"
                                                            type="button" @click="cancelExistingFields">
                                                            <div
                                                                class="d-flex justify-content-center align-items-center">
                                                                <div class="f-game fs-24">Cancel</div>
                                                            </div>
                                                        </Button>

                                                        <Button class="blue rounded-md p-1 px-5 mx-2" type="button"
                                                            :loading="loadingCopy" :disabled="loadingCopy"
                                                            @click="copyField(steps[currentStepRef]?.value.step)">
                                                            <div
                                                                class="d-flex justify-content-center align-items-center">
                                                                <div class="f-game fs-24">Choose</div>
                                                            </div>
                                                        </Button>

                                                    </div>

                                                </GoalCardWrapper>
                                            </Motion>
                                        </Presence>

                                    </template>
                                </template>

                            </template>

                        </FieldArray>

                        <Button class="blue mt-6 rounded-md mx-auto d-table p-1 ps-3 pe-2" type="button"
                            v-on="currentStepRef === 5 ? { click: onPublish } : { click: onSubmitNextStep }"
                            :disabled="(!isEveryStepHasQuestions && currentStepRef != currentStep) || stepLoading"
                            :loading="stepLoading" v-if="!goal?.is_published || currentStepRef < 5">
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="f-game fs-24">{{ currentStep === 5 ? 'Publish' : 'Add Step' }}</div>
                                <Ionicon :icon_name="currentStep === 5 ? 'chevron-forward-outline' : 'add-outline'"
                                    class="ms-1" font_size="24" />
                            </div>
                        </Button>

                        <Button class="red mt-6 rounded-md mx-auto d-table p-1 ps-3 pe-2" type="button"
                            :loading="buttonPublishStatus.loading && isClicked == 2"
                            :is_success="buttonPublishStatus.success && isClicked == 2"
                            :is_fail="buttonPublishStatus.fail && isClicked == 2"
                            v-on="true ? { click: onUnpublish } : {}" :disabled="buttonPublishStatus.disabled" v-else>
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="f-game fs-24">Unpublish</div>
                                <Ionicon icon_name="chevron-forward-outline" class="ms-1" font_size="24" />
                            </div>
                        </Button>

                    </div>
                </div>
            </div>
        </div>
    </form>

    <div class="container">
        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">Set this quest to be a promo quest</div>
            <div class="fs-14 grey text-sm-bold">A promo quest is a highlighted quest. It can be used to highlight
                something
                that you want to feature through quests like registrations for events, programs etc.</div>
            <BaseInputSwitch name="promo_goal" label="Set as a promo quest" class="mt-3" ref="toggleRef"
                @onSwitchChange="setAsPromoGoal" />
        </div>
        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">Set this quest to be part of users' onboarding</div>
            <div class="fs-14 grey text-sm-bold">A users' onboarding is a process before users have access to the
                platform,
                this quest is suitable to collect personal data or mandatory questions.</div>
            <BaseInputSwitch name="onboarding_goal" label="Set as an onboarding quest" class="mt-3" ref="toggleRef"
                @onSwitchChange="setAsOnboardingGoal" />
        </div>
        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">How do quests work?</div>
            <div class="fs-14 grey text-sm-bold">To create a quest, you must assign a level and category to it.
                After that you need to follow the flow where you add quest details, rewards, steps and fields.
                Publish the quest for startups for them to complete and receive the rewards.</div>
        </div>
        <div class="mb-8">
            <Accordion :accordion-data="accordionObj" />
        </div>
    </div>
</template>

<script setup>
import PageExplanation from "@/components/PageExplanation.vue"
import NumberTextCircle from "@/components/Goals/NumberWithCircle.vue"
import Image from "@/components/Image.vue"
import LevelBar from "@/components/Goals/LevelBar.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import Button from "@/components/Button.vue"
import Badge from "@/components/Badge.vue"
import Steps from "@/components/Goals/Steps.vue"
import Ionicon from "@/components/Ionicon.vue"
import { useGoalsStore } from "@/stores/goalsStore"
import { adminAuthStore } from "@/stores/adminAuthStore"
import { storeToRefs } from 'pinia';
import { computed, onMounted, ref, reactive, markRaw, inject, watch } from "vue"
import { watchDebounced, useDebounceFn } from '@vueuse/core'
import Accordion from "@/components/Accordion.vue";
import Goal from "@/components/Goals/Goal.vue"
import Step from "@/components/Goals/Step.vue"
import { useForm, useFieldArray, FieldArray, useField } from 'vee-validate';
import { number, mixed, string } from "yup";
import { Motion, Presence } from "motion/vue"
import { useRouter, useRoute, onBeforeRouteLeave } from "vue-router";
import Swal from 'sweetalert2'
import { Tooltip } from "bootstrap/dist/js/bootstrap.esm.min.js";
import delay from 'delay';
import PQueue from 'p-queue';

//======= Dynamic form fields
import GoalCardWrapper from "@/components/Goals/GoalCardWrapper.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseDatePicker from "@/components/Form/BaseDatePicker.vue"
import BaseTextarea from "@/components/Form/BaseTextarea.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import BaseInputCheckbox from "@/components/Form/BaseInputCheckbox.vue"
import BasePickSlider from "@/components/Form/BasePickSlider.vue"
import BaseFileUploader from "@/components/Form/BaseFileUploader.vue"
import BaseTextareaAutosize from "@/components/Form/BaseTextareaAutosize.vue"
import BaseSelectwithOption from "@/components/Form/CombinedFields/BaseSelectwithOption.vue"
import BaseSelectForSlider from "@/components/Form/CombinedFields/BaseSelectForSlider.vue"
import BaseSwitchForFileUpload from "@/components/Form/CombinedFields/BaseSwitchForFileUpload.vue"
import BaseTags from "@/components/Form/BaseTags.vue"

import ShortTextInput from "@/components/Form/BaseQuestions/ShortTextInput.vue"
import LongTextInput from "@/components/Form/BaseQuestions/LongTextInput.vue"
import CanvasInput from "@/components/Form/BaseQuestions/CanvasInput.vue"
import SwitchInput from "@/components/Form/BaseQuestions/SwitchInput.vue"
import Slider from "@/components/Form/BaseQuestions/Slider.vue"
import RadioInput from "@/components/Form/BaseQuestions/RadioInput.vue"
import DropdownInput from "@/components/Form/BaseQuestions/DropdownInput.vue"
import DateInput from "@/components/Form/BaseQuestions/DateInput.vue"
import UploadFile from "@/components/Form/BaseQuestions/UploadFile.vue"
import CountrySelect from "@/components/Form/BaseQuestions/CountrySelect.vue"
import PhoneSelect from "@/components/Form/BaseQuestions/PhoneSelect.vue"

/*==================  end Dynamic form fields  ===================*/
const axios = inject("axios");
const router = useRouter();
const route = useRoute();
const { adminAuth, programGroup } = storeToRefs(adminAuthStore());
const { fetchProgramGroup } = adminAuthStore();
const { goal, preview_goal } = storeToRefs(useGoalsStore());
const { fetchEditGoal, PublishEditGoal, UpdateSaveGoal, addStep,
    addField, deleteStep, UnpublishEditGoal, GetListQuestions, copyQuestion } = useGoalsStore();
const { currentProgramId, selectProgramRef } = inject('programSelection')
const { handleSubmit, setFieldValue, values, errors, resetForm, validate: validateForm, meta: metaForm } = useForm({
    initialValues: {
        steps: [
            {
                is_step_complete: false,
                cards: [
                    {

                    }
                ]
            }
        ],
    },
    keepValuesOnUnmount: false,
    validateOnMount: false
});
watch(currentProgramId, (newValue, oldValue) => {
    setFieldValue("goal_program_id", newValue);
})
let { remove: stepRemove, update: stepUpdate, push: stepPush, fields: steps, replace: stepReplace } = useFieldArray('steps');
const { remove: cardRemove, update: cardUpdate, push: cardPush, fields: veeCards } = useFieldArray('cards');
const stepsData = computed(() => {
    return steps.value?.slice(1);
});

const isEveryStepHasQuestions = computed(() => {
    if (!steps.value) {
        return false;
    }
    return steps.value
        .map((step) => Object.keys(step.value?.cards ?? {}).length > 0)
        .every((element) => element === true) && steps.value.length > 0;
});

const selectLevel = ref(0);
const currentStep = computed(() => {
    return steps.value?.length - 1;
});

const currentStepRef = ref(currentStep.value);
const currentStepTitleText = computed(() => {
    return steps.value[currentStepRef.value]?.value.step_title;
});
const currentStepDescriptionText = computed(() => {
    return steps.value[currentStepRef.value]?.value.step_description;
});

const stepsByCurrentIndex = computed(() => {
    return steps.value[currentStep]
});
const stepLoading = ref(false);
const onSubmitNextStep = handleSubmit(async values => {
    stepLoading.value = true;
    try {
        await saveChanges();
        await addStep(values.goal_id);
        await fetchEditGoal(route.params.id);
        stepReplace(goal.value.steps);
        setFieldValue(`steps[${currentStep.value - 1}].is_step_complete`, true);
        currentStepRef.value = currentStep.value;
        document.getElementById('form-section').scrollIntoView();
    } catch (e) {
        console.error('onSubmitNextStep error:', e);
    } finally {
        stepLoading.value = false;
    }

}, ({ errors }) => {

    const fieldName = Object.keys(errors)[0];
    console.log(errors)
    const idField = String(fieldName).replace(/[^0-9a-z]/gi, '');
    document.getElementById(idField)?.scrollIntoView({
        block: 'center',
        behavior: 'smooth',
    });
    document.getElementById(idField)?.focus();
    // targetScroll(fieldName, 160);

});

const buttonPublishStatus = reactive({
    loading: false,
    success: false,
    fail: false,
    disabled: false
});

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 5000,
    timerProgressBar: true,
    showCloseButton: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

const onPublish = handleSubmit(async values => {
    try {
        setFieldValue("is_published", true);
        let formData = new FormData();
        if (goalImageRef.value?.files[0] !== undefined) {
            formData.append('file', goalImageRef.value?.files[0], goalImageRef.value?.files[0]?.name)
        }
        formData.append('data', JSON.stringify(values));
        formData.append('programId', adminAuth.value.programId);

        buttonPublishStatus.loading = true;
        buttonPublishStatus.disabled = true;
        stepLoading.value = true;
        isClicked.value = 2;

        await new Promise(resolve => setTimeout(resolve, 1000));

        const res = await PublishEditGoal(formData);
        if (res.status === 200) {
            buttonPublishStatus.loading = false;
            buttonPublishStatus.success = true;
            stepLoading.value = false;
            Toast.fire({
                icon: 'success',
                title: 'The quest was published successfully',
                background: '#ACE7A0',
                iconColor: '#5BBA47',
                color: '#258212'
            });

            fetchEditGoal(route.params.id);
            // return router.push("/goals");
        } else {
            buttonPublishStatus.loading = false;
            buttonPublishStatus.fail = true;
            stepLoading.value = false;
        }
    } catch (error) {
        console.error(error);
    } finally {
        await new Promise(resolve => setTimeout(resolve, 1000));
        buttonPublishStatus.disabled = false;
        buttonPublishStatus.fail = false;
        buttonPublishStatus.success = false;
        stepLoading.value = false;
        isClicked.value = 0;
    }
}, ({ errors }) => {
    buttonPublishStatus.loading = false;
    buttonPublishStatus.disabled = false;
    const fieldName = Object.keys(errors)[0];
    const idField = String(fieldName).replace(/[^0-9a-z]/gi, '');

    document.getElementById(idField).scrollIntoView();
    document.getElementById(idField)?.focus();
});

const onUnpublish = async () => {
    try {
        let formData = new FormData();
        formData.append('data', JSON.stringify(values));

        buttonPublishStatus.loading = true;
        buttonPublishStatus.disabled = true;
        isClicked.value = 2;
        setFieldValue("is_published", false);

        const res = await UnpublishEditGoal(formData);
        if (res.status === 200) {
            buttonPublishStatus.loading = false;
            buttonPublishStatus.success = true;
            Toast.fire({
                icon: 'success',
                title: 'The quest was Unpublished successfully',
                background: '#ACE7A0',
                iconColor: '#5BBA47',
                color: '#258212'
            });
            fetchEditGoal(route.params.id);
        } else {
            buttonPublishStatus.loading = false;
            buttonPublishStatus.fail = true;
        }
    } catch (error) {
        console.error(error);
    } finally {
        await new Promise(resolve => setTimeout(resolve, 1000));
        buttonPublishStatus.disabled = false;
        buttonPublishStatus.fail = false;
        buttonPublishStatus.success = false;
        isClicked.value = 0;
    }
}

const isClicked = ref(0);
const textSave = ref("");

const saveChanges = async () => {
    let formData = new FormData();
    if (goalImageRef.value?.files[0] !== undefined) {
        formData.append(
            "file",
            goalImageRef.value?.files[0],
            goalImageRef.value?.files[0]?.name
        );
    }
    formData.append("data", JSON.stringify(values));
    formData.append("programId", adminAuth.value.programId);
    formData.append("goal_id", route.params.id);

    buttonPublishStatus.disabled = true;
    buttonPublishStatus.loading = true;
    isClicked.value = 1;
    textSave.value = "Saving your changes...";
    if (!!formData.entries().next().value) {
        try {
            await UpdateSaveGoal(formData);
            const checkIfCardsIdExist = values.steps.map((i) => {
                if (i.cards != undefined) {
                    return i.cards.map((x) => x.id);
                } else {
                    return [];
                }
            });
            const AnyUndefinedCardsId = checkIfCardsIdExist.flat().includes(undefined);
            if (
                values.steps.some((goal) => goal.step === undefined) ||
                AnyUndefinedCardsId
            ) {
                if (route.params.id != undefined) {
                    await fetchEditGoal(route.params.id);
                    stepReplace(goal.value.steps);
                }
            }
        } finally {
            setTimeout(() => {
                buttonPublishStatus.disabled = false;
                buttonPublishStatus.loading = false;
                textSave.value = "";
                isClicked.value = 0;
            }, 3000);
        }
    }
};

const onPreview = async () => {
    let route = router.resolve({ path: "/preview-quest" });
    window.open(route.href);
};

const programGroupList = computed(() => {
    return [
        {
            programGroupId: 0,
            groupName: "No Group"
        },
        ...programGroup.value
    ];
});

onMounted(async () => {
    
    if (route.query.programid != undefined && route.query.programid > 0 && adminAuth.value.role == "Configuration Access") {
        adminAuth.value.programId = Number(route.query.programid);
        selectProgramRef.value?.handleChange(Number(route.query.programid));
    }

    new Tooltip(document.body, {
        selector: "[data-bs-toggle='tooltip']",
        trigger: "hover",
    });

    await fetchEditGoal(route.params.id);
    await fetchProgramGroup(adminAuth.value.programId);

    resetForm({
        values: {
            level_code_number: goal.value.code_level,
            level_id: goal.value.id_level,
            goal_title: goal.value.title,
            goal_subtitle: goal.value.second_title,
            goal_description: goal.value.description,
            goal_help_text: goal.value.goal_help_text,
            goal_reward: goal.value.total_points,
            goal_label: goal.value.label != "[]" ? goal.value?.label?.replace(/\[|\]|\r|\n|\s+|"/g, '').split(',') : [],
            goal_img: goal.value.logo != "" ? goal.value.logo : null,
            goal_id: route.params.id,
            promo_goal: goal.value.promo_goal,
            onboarding_goal: goal.value.onboarding_goal,
            is_published: goal.value.is_published,
            status_score: goal.value.status_score,
            program_group_id: goal.value.program_group_id,
            file_url: `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(goal.value.logo.trim())}`
        }
    });

    stepReplace(goal.value.steps);
    categoryOptions.value = categoryIdOptions[goal.value.code_level];
    selectLevel.value = goal.value.code_level;
    currentStepRef.value = currentStep.value;
    goalReward.value = goal.value.total_points;
    imgUrl.value = goal.value.logo != "" ? `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(goal.value.logo.trim())}` : null;

    preview_goal.value = values;

});

const AuthUser = adminAuthStore();
watch(
      () => adminAuth.value.programId, 
      async (newValue, oldValue) => {
        await fetchProgramGroup(adminAuth.value.programId);
      }
    );

const queue = new PQueue({ concurrency: 1 });

watchDebounced(
    values,
    async () => {
        if (existingFieldsIndex.value == undefined) {
            await saveChanges();
            preview_goal.value = values;
        }
    },
    {
        debounce: 1000, maxWait: 5000
    },
);

window.onbeforeunload = function () {
    saveChanges();
};

onBeforeRouteLeave((to, from, next) => {
    saveChanges();
    next();
});

const activeStep = computed(() => {
    return steps.value.find((element, index) => {
        if (index != currentStep.value) {
            return !element.value.is_step_complete;
        } else {
            return steps.value[index];
        }
    });
});

const activeStepIndex = computed(() => {
    return steps.value.indexOf(activeStep.value);
});

const viewStep = async (stepIndex) => {

    // const { valid, errors } = await validateForm();

    // if (valid) {
    //     currentStepRef.value = stepIndex;
    // }
    document.getElementById('form-section').scrollIntoView();
    currentStepRef.value = stepIndex;
}

const setAsPromoGoal = async (status_promo) => {
    if (status_promo) {
        if (confirm('You can only set 1 quest to be a promo quest, Are you sure you want to set this quest to be a promo quest ?')) {
            const { data, status } = await axios.post(`/api/Goals/SetAsPromoGoal`);
        }
    }
}

const setAsOnboardingGoal = async (status_onboarding) => {
    if (status_onboarding) {
        if (confirm('You can only set 1 quest to be an onboarding quest, Are you sure you want to set this quest to be an onboarding quest ?')) {
            const { data, status } = await axios.post(`/api/Goals/setAsOnboardingGoal?goalId=` + route.params.id);
        }
    }
}

const enableScoring = () => {

}

const dynamicFields = {
    2: markRaw(ShortTextInput),
    3: markRaw(DropdownInput),
    4: markRaw(RadioInput),
    5: markRaw(SwitchInput),
    6: markRaw(DateInput),
    7: markRaw(Slider),
    8: markRaw(UploadFile),
    9: markRaw(CanvasInput),
    10: markRaw(LongTextInput),
    11: markRaw(PhoneSelect),
    12: markRaw(CountrySelect),
}

const pickFieldLoading = ref(false);
const pickFieldSelectedId = ref();

const pickField = async (btnIndex, stepId, fieldCode) => {
    if (stepId != undefined) {

        pickFieldLoading.value = true;
        pickFieldSelectedId.value = btnIndex;

        await saveChanges();
        await addField(stepId, fieldCode);
        await fetchEditGoal(route.params.id);
        stepReplace(goal.value.steps);

        // updateFieldIndex.value = null;
        pickFieldLoading.value = false;
        pickFieldSelectedId.value = null;
    }
};

const updateLoading = ref(false);
const UpdateField = async (cardId, updateFieldIndex, btnIndex, fieldCode) => {
    if (cardId > 0) {
        updateLoading.value = true;
        const { data, status } = await axios.post(`/api/Goals/UpdateCard?cardId=${cardId}&fieldCode=${fieldCode}`);

        if (status == 200) {
            updateLoading.value = false;
            fetchEditGoal(route.params.id).then(() => {
                stepReplace(goal.value.steps);
                pickField(updateFieldIndex, btnIndex);
            });
        } else {
            updateLoading.value = false;
        }
    }
}

const deleteStepRef = ref();
const deleteStepLoading = ref(false);
const deleteStepId = ref();
const DeleteStep = async (id, index) => {
    if (confirm('Are you sure want to delete this Step?')) {
        if (deleteStepRef.value[index]?.btnRef?.id == "btn-delete") {
            deleteStepLoading.value = true;
            let deleteStepId = index;
            await saveChanges();
            await deleteStep(id);
            await fetchEditGoal(route.params.id);
            stepReplace(goal.value.steps);
            currentStepRef.value = currentStep.value;
            deleteStepLoading.value = false;
            deleteStepId = null;
        }
    }
};

const deleteField = async (cardId) => {
    if (cardId > 0) {
        return await axios.post(`/api/Goals/DeleteCard?cardId=${cardId}`);
    }
}

const deletePickedField = async (cardId) => {
    if (confirm('Are you sure want to delete this Question?')) {
        if (cardId != undefined) {
            await saveChanges();
            await deleteField(cardId);
            await fetchEditGoal(route.params.id);
            stepReplace(goal.value.steps);
        }
    }
};

const updateFieldIndex = ref(null);

const showUpdatePickedField = (selectedIndex) => {
    updateFieldIndex.value = updateFieldIndex.value == selectedIndex ? null : selectedIndex;
};

let existingFieldsIndex = ref();
let questionList = ref([]);
let itemShowPerPage = ref(12);
let showEntries = ref();
let searchInput = ref();
let currentPage = ref(1);
let totalPage = ref(0);
let existingFieldActiveIndex = ref();
let existingFieldActiveObj = ref();

const onClickExistingField = (id, question) => {
    existingFieldActiveIndex.value = id;
    existingFieldActiveObj.value = question;
}


const showExistingFields = async (selectedIndex) => {
    questionList.value = [];
    existingFieldsIndex.value = existingFieldsIndex.value == selectedIndex ? undefined : selectedIndex;
    setFieldValue('search', '');
    const questions = await GetListQuestions("", showEntries?.value[0]?.selectRef?.selected, 1, route.params.id);
    questionList.value = questions?.resultData;
    totalPage.value = questions?.TotalPage;
    itemShowPerPage.value = showEntries.value[0]?.selectRef?.selected;
    existingFieldActiveIndex.value = undefined;
    existingFieldActiveObj.value = undefined;
}
const onSelectShowEntries = async (selected) => {

    const questions = await GetListQuestions(searchInput.value[0]?.inputValueRef.value, selected, 1, route.params.id);
    totalPage.value = questions?.TotalPage;
    questionList.value = questions?.resultData;
    currentPage.value = 1;
    itemShowPerPage.value = showEntries.value[0]?.selectRef.selected;
    existingFieldActiveIndex.value = undefined;
    existingFieldActiveObj.value = undefined;

}

const onClickHandler = async (number) => {
    currentPage.value = number;
    const questions = await GetListQuestions(searchInput.value[0]?.inputValueRef.value, showEntries.value[0]?.selectRef.selected, number, route.params.id);
    totalPage.value = questions?.TotalPage;
    questionList.value = questions?.resultData;
    existingFieldActiveIndex.value = undefined;
    existingFieldActiveObj.value = undefined;
};
const onInputSearch = useDebounceFn(async (text) => {

    const questions = await GetListQuestions(text.target.value, showEntries.value[0]?.selectRef.selected, 1, route.params.id);
    totalPage.value = questions?.TotalPage;
    questionList.value = questions?.resultData;
    currentPage.value = 1;
    itemShowPerPage.value = showEntries.value[0]?.selectRef.selected;
    existingFieldActiveIndex.value = undefined;
    existingFieldActiveObj.value = undefined;

}, 500);

const cancelExistingFields = () => {
    existingFieldsIndex.value = undefined;
    existingFieldActiveIndex.value = undefined;
    existingFieldActiveObj.value = undefined;
    questionList.value = [];
}
const cancelUpdatePickedField = () => {
    updateFieldIndex.value = null;
}

let loadingCopy = ref(false);
const copyField = async (stepId) => {
    loadingCopy.value = true;
    await copyQuestion(existingFieldActiveObj.value?.IdCard, stepId);
    await fetchEditGoal(route.params.id);
    stepReplace(goal.value.steps);
    existingFieldsIndex.value = undefined;
    existingFieldActiveIndex.value = undefined;
    existingFieldActiveObj.value = undefined;
    loadingCopy.value = false;
}

const goalBuilderObj = [
    {
        field_code: 2,
        field_code_type: 0,
        has_option: false,
        icon_url: '/goal-builder-icon/short-text.svg',
        btn_name: 'Short Text Input',
    },
    {
        field_code: 10,
        field_code_type: 0,
        has_option: false,
        icon_url: '/goal-builder-icon/long-text.svg',
        btn_name: 'Long Text Input',
    },
    {
        field_code: 9,
        field_code_type: 0,
        has_option: false,
        icon_url: '/goal-builder-icon/canvas-input.svg',
        btn_name: 'Canvas Input',
    },
    {
        field_code: 5,
        field_code_type: 0,
        has_option: true,
        icon_url: '/goal-builder-icon/toggle_on.svg',
        btn_name: 'Switch Input',
    },
    {
        field_code: 7,
        field_code_type: "",
        has_option: true,
        icon_url: '/goal-builder-icon/slider.svg',
        btn_name: 'Slider',
    },
    {
        field_code: 4,
        field_code_type: 0,
        has_option: true,
        icon_url: '/goal-builder-icon/long-text.svg',
        btn_name: 'Radio Input',
    },
    {
        field_code: 3,
        field_code_type: 0,
        has_option: true,
        icon_url: '/goal-builder-icon/dropdown.svg',
        btn_name: 'Dropdown Input',
    },
    {
        field_code: 6,
        field_code_type: 0,
        has_option: false,
        icon_url: '/goal-builder-icon/date-pick.svg',
        btn_name: 'Date Input',
    },
    {
        field_code: 8,
        field_code_type: 0,
        has_option: false,
        icon_url: '/goal-builder-icon/upload.svg',
        btn_name: 'Upload File',
    },
    {
        field_code: 12,
        field_code_type: 0,
        has_option: false,
        icon_url: '/goal-builder-icon/flag.svg',
        btn_name: 'Country Select',
    },
    {
        field_code: 11,
        field_code_type: 0,
        has_option: false,
        icon_url: '/goal-builder-icon/phone.svg',
        btn_name: 'Phone Select',
    }
];

/*==================  END HANDLE DYNAMIC FIELDS  ===================*/
const levelIdOptions = [2, 4];
// const disabledLevelId = [1, 5, 9, 13];
const categoryIdOptions = {
    2: [
        {
            id: 2,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-1.png",
            name: "Team",
            id_goal_category: 1
        },
        {
            id: 6,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-1.png",
            name: "Company",
            id_goal_category: 2
        },
        {
            id: 10,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-1.png",
            name: "Service",
            id_goal_category: 3
        },
        {
            id: 14,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-1.png",
            name: "Growth",
            id_goal_category: 4
        },

    ],
    3: [
        {
            id: 3,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-2.png",
            name: "Team",
            id_goal_category: 1
        },
        {
            id: 7,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-2.png",
            name: "Company",
            id_goal_category: 2
        },
        {
            id: 11,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-2.png",
            name: "Service",
            id_goal_category: 3
        },
        {
            id: 15,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-2.png",
            name: "Growth",
            id_goal_category: 4
        },
    ],
    4: [
        {
            id: 4,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-3.png",
            name: "Team",
            id_goal_category: 1
        },
        {
            id: 8,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-3.png",
            name: "Company",
            id_goal_category: 2
        },
        {
            id: 12,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-3.png",
            name: "Service",
            id_goal_category: 3
        },
        {
            id: 16,
            logo_link: "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-3.png",
            name: "Growth",
            id_goal_category: 4
        },
    ]
};

const levelId = ref(2);
const selectedCategory = ref();
const imgUrl = ref(null);
const goalImageRef = ref();

const categoryOptions = ref(null)
const goalReward = ref(0);
const rewardInputRef = ref();

const onSelectLevel = (id) => {
    levelId.value = id;
    categoryOptions.value = categoryIdOptions[id];

    const selectedLevel = id - 1;
    goalReward.value = selectedLevel > 2 ? (selectedLevel * 50) + 100 : (selectedLevel * 50);
    rewardInputRef.value.handleChange(goalReward.value);

    setFieldValue('level_id', "");
}


const FILE_SIZE = 1000 * 1000;
const SUPPORTED_FORMATS = [
    "image/jpg",
    "image/jpeg",
    "image/png"
];

const { value: fileValue, meta, errorMessage, handleChange, resetField, validate, errors: goalImgErrors, setValue } = useField('goal_img',
    mixed().required("An Image file is required").test('FILE_SIZE', "Image file is too big, Max 1MB.", (value) => {

        return (
            typeof value != 'string' ?
                (goalImageRef.value?.files[0]?.size <= FILE_SIZE ? true : false)
                : true)
    }).test('FILE_TYPE', "Image file has unsupported format.", (value) => {
        return (
            typeof value != 'string' ?
                (SUPPORTED_FORMATS.includes(goalImageRef.value?.files[0]?.type) ? true : false)
                : true)
    })
);

const { setValue: setFileName, handleChange: changeFileName } = useField('file_url');

const onGoalImageChange = async (e) => {
    const file = e.target?.files[0];
    setValue(file);
    await validate();

    if (goalImgErrors.value.length == 0) {
        let generatedFile = URL.createObjectURL(file);
        changeFileName(generatedFile);
        imgUrl.value = generatedFile;
        handleChange(file.name);
    } else {
        imgUrl.value = null;
        changeFileName(null);
    }
};

const addGoalImage = () => {
    goalImageRef.value.click();
};



const accordionObj = [
    {
        'title': 'What is a QUEST?',
        'content': 'A quest is a set of tasks startups need to complete in order to earn a reward (The Startup Buddy Coins). Your startups can use these coins for networking, access to events etc. Quests on the Dashboard can have up to 5 STEPS. Rewards are granted after completing all the steps for one quest. Forms, feedback, pitch deck uploads, registrations, events etc. can all be Quests'
    },
    {
        'title': 'What is a STEP?',
        'content': 'A step is a form of input that you require from the startups like company financial details, pitch decks, feedback etc. Each step can have up to 3 fields. You can choose from multiple field types (text, sliders, dropdown, dates, uploads, etc.). These fields are used to collect data, files, feedback, registrations, etc, from startups'
    },
    {
        'title': 'How to create a Quest?',
        'content': 'Click on the “Create” (link to create) button at the top of the screen. You will be redirected to the Quests section. Select a level and category for the quest. Then fill out the quest details and reward you want to provide after completion. Add steps to the quest and publish it so that startups can access and complete them'
    },
    {
        'title': 'What are levels and categories?',
        'content': ' Each quest can be divided into 3 levels (Lvl 1 - Lvl Max) based on their importance. Also, you can divide the quests into 4 categories (Company, Team, Service and Growth). Example: Important details about companies (valuation, funding etc.) will come under LVL 1 for Company, Team details will come under LVL 1 for Team, etc.)'
    }
]

</script>
<style scoped>
.stepper-wrapper {
    margin-top: auto;
    display: -webkit-box;
    display: -webkit-flex;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-pack: justify;
    -webkit-justify-content: space-between;
    -ms-flex-pack: justify;
    justify-content: space-between;
}

.stepper-item {
    position: relative;
    display: -webkit-box;
    display: -webkit-flex;
    display: -ms-flexbox;
    display: flex;
    -webkit-box-orient: vertical;
    -webkit-box-direction: normal;
    -webkit-flex-direction: column;
    -ms-flex-direction: column;
    flex-direction: column;
    -webkit-box-align: center;
    -webkit-align-items: center;
    -ms-flex-align: center;
    align-items: center;
    -webkit-box-flex: 1;
    -webkit-flex: 1;
    -ms-flex: 1;
    flex: 1;
}

@media (max-width: 768px) {
    .stepper-item {
        font-size: 12px;
    }
}

.stepper-item::before {
    position: absolute;
    content: "";
    width: 100%;
    top: 4.5rem;
    left: -50%;
    z-index: 2;
    height: 5px;
    margin-bottom: 1rem;
    overflow: hidden;
    font-size: .75rem;
    font-weight: 600;
    -webkit-box-shadow: inset 2px 2px 5px #b8b9be, inset -3px -3px 7px #fff;
    box-shadow: inset 2px 2px 5px #b8b9be, inset -3px -3px 7px #fff;
    background-color: #fafbfe;
    -webkit-border-radius: 100px;
    border-radius: 100px;
}

.step-counter {
    position: relative;
    z-index: 5;
}

.stepper-item.completed::after {
    position: absolute;
    content: "";
    width: 100%;
    top: 4.5rem;
    height: 5px;
    left: 50%;
    z-index: 3;
    background: -o-linear-gradient(132.3deg, rgba(144, 88, 51, 0.4) 0%, rgba(255, 255, 255, 0.4) 105.18%), #FFE3B0;
    background: linear-gradient(317.7deg, rgba(144, 88, 51, 0.4) 0%, rgba(255, 255, 255, 0.4) 105.18%), #FFE3B0;
    background-blend-mode: soft-light, normal;
    border: 1px solid rgba(255, 255, 255, 0.4);
    -webkit-box-shadow: inset -2.5px -2.5px 5px rgb(250 251 255 / 10%), inset 2.5px 2.5px 5px #905833;
    box-shadow: inset -2.5px -2.5px 5px rgb(250 251 255 / 10%), inset 2.5px 2.5px 5px #905833;
    -webkit-border-radius: 50px;
    border-radius: 50px;
}

.stepper-item:first-child::before {
    content: none;
}

.stepper-item:last-child::after {
    content: none;
}

.goal-details-hero {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    background-size: cover;
    background-attachment: scroll;
    height: auto;
    position: relative;
}

.goal-tasks-wrapper {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    height: auto;
    min-height: 500px;
    padding: 4rem;
    background-size: cover;
}

.step-number {
    border-radius: 50%;
    width: 48px;
    height: 48px;
    padding: 12px;
    text-align: center;
    background: #c2c2c2;
    border: 1px solid #c2c2c2;
    color: #fff;
}

.complete .step-number {
    background: #FFE3B0;
    border: 1px solid #C69030;
    color: #C69030;
}

.current {
    background: #416094;
    box-shadow: 0px 4px 8px rgba(65, 96, 148, 0.35);
    border-radius: 1rem;
}

.current .step-number {
    background: #fff;
    border: 1px solid #DFAE55;
    color: #C69030;
    font-weight: 900;
}

.current .step-title {
    color: #fff;
}

.step-title {
    max-height: 24px;
    line-height: 1.5rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}

.step-title-sm {
    max-height: 48px;
    line-height: 1rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}

.current .step-info {
    color: #FFE3B0;
}

.field-menu-control {
    position: absolute;
    right: 0;
}

.sticky-step-left {
    top: 144px;
    z-index: 9;
}

.goal-img-input {
    position: absolute;
    right: 0;
    display: none;
}

.add-goal-text {
    position: absolute;
    right: 0;
    cursor: pointer;
    user-select: none;
}
</style>