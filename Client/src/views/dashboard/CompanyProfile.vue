<template>
    <PageExplanation class="no-toggle" :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`" :logo="adminAuth?.photo">
        <template v-slot:top-content>
            <div class="d-flex justify-content-start align-items-center gap-3">
                <Image
                    :src="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${companyGoal?.startup?.startupLogo}`"
                    alt="DP" class="rounded-lg shadow" width="35" height="35"
                    :class="{ invisible: companyGoal == undefined }" />
                <div class="f-game fs-24 text-info">{{ companyGoal?.startup?.Name }}</div>
            </div>
        </template>
        <!-- <template v-slot:body-content>
            <div>
                <div class="row align-items-start content-inside user-select-none py-3">
                    <div class="col-6">
                        <h3 class="text-sm-bold fs-24 mb-3">Company Overview</h3>
                        <p class="text-sm-bold fs-14 grey">Now, for interaction-heavy pages, smaller text sizes are
                            perfectly
                            acceptable. In fact, depending on the amount of data your user is taking in at once, even 18px
                            text
                            is Now, for interaction-heavy pages, smaller text sizes are perfectly acceptable. In fact,
                            depending
                            on the amount of data your user is taking in at once, even 18px text isNow.</p>
                    </div>
                    <div class="col-6">
                        <h3 class="text-sm-bold fs-24 mb-3">Extra Info</h3>
                        <div class="row">
                            <div class="col-6">
                                <div class="d-flex justify-content-start align-items-center mb-3">
                                    <h3 class="text-info text-sm-bold fs-16 mb-0 me-3">Status :</h3>
                                    <Badge class="goal-label green">Series A</Badge>
                                </div>
                                <div class="d-flex justify-content-start align-items-center mb-3">
                                    <h3 class="text-info text-sm-bold fs-16 mb-0 me-3">Market :</h3>
                                    <p class="text-sm-bold fs-14 grey mb-0">Psychometrics, Entertainment</p>
                                </div>
                                <div class="d-flex justify-content-start align-items-center">
                                    <h3 class="text-info text-sm-bold fs-16 mb-0 me-3">Region :</h3>
                                    <p class="text-sm-bold fs-14 grey mb-0">European Union</p>
                                </div>
                            </div>
                            <div class="col-6">
                                <h3 class="text-info text-sm-bold fs-16 mb-3 me-3">Revenue :</h3>
                                <div class="shadow-in--sm radius-16 p-2 text-center mb-3">
                                    <p class="mb-0 text-bold fs-16"><span class="text-green">0</span> <span
                                            class="text-info">/
                                            y</span></p>
                                </div>
                                <Button class="green rounded-md p-1 px-3">
                                    <div class="d-flex justify-content-center align-items-center">
                                        <div class="f-game fs-24">Connect</div>
                                    </div>
                                </Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </template> -->
    </PageExplanation>
    <div class="container">
        <div class="row">
            <div class="col-3">
                <div class="bg-white shadow-out--sm ps-5 pb-4 pe-5 pt-4 radius-16 my-6">
                    <div class="text-center">
                        <p class="f-game fs-24 base-grey mb-4">Company Overview</p>
                    </div>
                    <div class="d-flex justify-content-center align-items-center mb-4">
                        <div class="w-100">
                            <p class="text-sm-bold fs-16 text-info">Leaderboard</p>
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-4 align-items-center point-info">
                                <div class="text-green fs-24"><span class="text-bold"> {{ companyGoal?.ranking }}</span><span
                                        class="base-grey fs-14"> / {{ companyGoal?.totalCompany }}</span></div>
                                <Image src="/trophy.svg" height="24" width="24"></Image>
                            </div>
                            <span class="f-game fs-20 base-grey">Total score : {{ companyGoal?.score }}</span>
                        </div>
                    </div>
                    <div class="d-flex justify-content-center align-items-center mb-4">
                        <div class="w-100">
                            <p class="text-sm-bold fs-16 text-info">TSB Coins</p>
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-4 align-items-center point-info">
                                <div class="text-warning fs-24 text-bold">{{ companyGoal?.coins?.toLocaleString("en-US")
                                    }}
                                </div>
                                <Image src="/coin.svg" height="24" width="24"></Image>
                            </div>
                        </div>
                    </div>
                    <div class="d-flex justify-content-center align-items-center mb-4"
                        v-if="startupInfo?.members?.length > 0">
                        <div class="w-100">
                            <p class="text-sm-bold fs-16 text-info">Team</p>
                            <div class="avatar-group">
                                <div class="avatar-more" v-if="startupInfo?.members?.length > 5">+{{
                        startupInfo?.members?.length - 5 }}</div>
                                <template v-for="(value, index) in startupInfo?.members?.slice(0, 5)" :key="index">
                                    <Image v-if="value"
                                        :src="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${value?.Photo}`"
                                        alt="DP" class="rounded-lg shadow" width="35" height="35" />
                                    <Image v-else src="https://www.w3schools.com/howto/img_avatar.png" alt="DP"
                                        class="rounded-lg shadow" width="35" height="35" />
                                </template>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div class="bg-white shadow-out--sm ps-5 pb-4 pe-5 pt-4 radius-16 mt-6">
                    <div class="d-flex justify-content-between align-items-start mb-5">
                        <div class="d-flex align-items-center">
                            <div class="flex-shrink-0">
                                <div class="shadow-out--sm p-3 rounded-md">
                                    <Ionicon icon_name="calendar-clear-outline" font_size="32" class="grey" />
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <div class="d-flex justify-content-start align-items-center gap-3">
                                    <div class="f-game fs-24 text-md text-info">ACTIVITY</div>
                                    <div class="cursor-pointer" data-bs-toggle="dropdown" aria-expanded="false">
                                        <Ionicon icon_name="ellipsis-vertical-circle-sharp" class="grey"
                                            font_size="32" />
                                    </div>
                                    <ul class="dropdown-menu" style="min-width: auto;">
                                        <li v-for="(time, index) in timePeriod" @click="onTimePeriodSelect(time.value)">
                                            <span class="fs-14 cursor-pointer dropdown-item">{{ time.label }}</span>
                                        </li>
                                    </ul>
                                </div>
                                <div class="fs-14 text-info text-md">{{ timePeriodName }}</div>
                            </div>
                        </div>
                        <div class="d-flex justify-content-start">
                            <div class="text-center me-5">
                                <div class="fs-14 base-grey-1">
                                    Steps last period
                                </div>
                                <div class="text-bold fs-16 text-sm-bold base-grey-1">
                                    {{ personalGraph?.stepLastTime }}
                                </div>
                            </div>
                            <div class="text-center">
                                <div class="fs-14 base-grey-1">
                                    Steps this period
                                </div>
                                <div class="text-bold fs-16 text-sm-bold base-grey-1">
                                    {{ personalGraph?.stepThisTime }}
                                </div>
                            </div>
                        </div>
                    </div>
                    <LineChart :chartData="chartData" :options="options" />
                </div>
            </div>
        </div>

        <div class="bg-white shadow-out--sm py-5 px-5 radius-16 text-center my-6 menu-company">
            <div class="d-flex justify-content-between align-items-start">
                <div class="text-center flex-fill cursor-pointer" v-for="(menu, index) in visibleMenu"
                    :class="{ active: menu.active }" @click="menuClicked(index)" :key="index">
                    <div class="f-game fs-24" :class="[menu.active ? 'text-white' : 'text-info']">{{ menu.name }}</div>
                    <Ionicon :icon_name="menu.icon" class="p-4" font_size="52"
                        :class="[menu.active ? 'white' : 'info']" />
                </div>
            </div>
        </div>

        <template v-if="activeMenu == 'Quests'">
            <div class="row mb-6 gx-5" v-if="companyGoal?.companyData?.length > 0" id="menu">
                <div class="col-4">
                    <div class="bg-white p-3 body-content radius-16 shadow-out--sm sticky-step-left sticky-top"
                        @click.stop="">

                        <div class="text-sm-bold fs-14 mb-3 d-flex">Quest Completed<div class="grey ms-2">{{ selectedGoal
                        + 1
                                }}/{{ companyGoal?.companyData?.length }}</div>
                        </div>

                        <template v-for="(goal, index) in companyGoal?.companyData">
                            <span class="f-game fs-18 text-info">{{ goal.title }}</span>
                            <div class="d-flex align-items-center my-2 me-3 cursor-pointer user-select-none mb-3"
                                :class="{ 'current': index == selectedGoalIndex }" @click.stop="selectGoal(index)">
                                <div class="flex-shrink-0">
                                    <Image
                                        :src="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(goal.logo.trim())}`"
                                        width="88" height="64" class="radius-16" />
                                </div>
                                <div class="flex-grow-1 ms-3">
                                    <div v-if="index == selectedGoalIndex">
                                        <p class="text-sm-bold fs-14 mb-0 text-white">{{ selectedGoalStepObj?.step_title
                                            }}
                                        </p>
                                        <span class="text-sm-bold fs-14 text-warning-light-2">{{ selectedGoalStepIndex
                                            }}/{{
                        selectedGoalStepLength - 1 }}</span>
                                    </div>
                                    <div v-else>
                                        <p class="text-sm-bold fs-14 mb-0">{{ goal?.steps[1].step_title }}</p>
                                        <span class="text-sm-bold fs-14 grey">{{ 1 }}/{{ goal?.steps?.length - 1
                                            }}</span>
                                    </div>
                                </div>
                                <div class="flex-grow-2 me-3 arrow-btn" @click.stop="goalStepsMenuClicked"
                                    v-if="index == selectedGoalIndex && goal?.steps.length > 2">
                                    <Ionicon
                                        :icon_name="index == selectedGoalIndex && isGoalStepsMenuClicked ? 'chevron-down' : 'chevron-forward'"
                                        class="grey-light-1 arrow-icon" font_size="32" />
                                </div>
                            </div>
                            <div v-if="index == selectedGoalIndex && isGoalStepsMenuClicked" class="my-4">
                                <template v-for="(step, index) in companyGoal?.companyData[selectedGoal]?.steps">
                                    <span class="d-block mb-1 cursor-pointer fs-14"
                                        v-if="index != selectedGoalStepIndex && index != 0"
                                        @click="selectGoalStep(index)"> Step :
                                        {{ index }}/{{ goal?.steps.length - 1 }} {{ step.step_title }}
                                    </span>
                                </template>
                            </div>
                        </template>
                        <!-- <pre>{{ selectedGoalCardFieldsObj }}</pre> -->
                    </div>
                </div>
                <div class="col-8">
                    <div class="company-goal-wrapper radius-16 shadow-out--sm"
                        :style="[selectedGoalObj?.logo != null ? { 'background-image': 'url(' + `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${encodeURIComponent(selectedGoalObj?.logo?.trim())}` + ')' } : { 'background-color': '#c2c2c2' }]">
                        <div class="radius-16 bg-white px-5 pb-5 pt-4 shadow-out--sm">

                            <div class="f-game fs-24 text-center mb-6">{{ selectedGoalStepObj?.step_title }} {{
                        selectedGoalStepIndex }}/{{ selectedGoalStepLength - 1 }}</div>
                            <div>

                                <div class="mb-6" v-for="(card, card_index) in selectedGoalCardFieldsObj"
                                    :key="card_index">

                                    <p class="text-sm-bold fs-16 mb-3 text-center">{{ card?.schema?.props?.label }}<span
                                            class="text-danger" v-if="card?.schema?.props?.required">*</span></p>

                                    <component :is="readOnlyFields[card?.schema?.field_code]" v-bind="{
                        value: card?.schema?.props?.value,
                    }" class="mb-3" />

                                    <div class="base-pick-slider" v-if="card.origin_score > 0"
                                        :class="{ 'div-disabled': onLoading }">
  
                                        <slider v-model="card.score" :min="0" :max="card.origin_score"
                                            @drag-end="onUpdateScore($event, card)" :height="16" :handleScale="1"
                                            :circleGap="10"
                                            v-if="scoreBarShow" 
                                            />
                                    </div>
                                    <p class="f-game fs-24 base-grey text-center">Score : {{ card.score }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <template v-else>
                <p class="f-game fs-24 base-grey text-center">No Quests Data</p>
            </template>
        </template>

        <template v-if="activeMenu == 'Teams'">
            <div class="container my-6">
                <div class="row d-flex justify-content-center">
                    <div class="col-12 px-0">
                        <div class="bg-white p-3 body-content radius-16 shadow-out--sm">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <h1 class="f-game fs-32 base-grey mb-0">Team Members</h1>
                                <span class="fs-14 base-grey">Owner: {{ companyGoal?.startup?.Email }}</span>
                            </div>
                            <div class="row">

                                <div class="col-12 col-lg-3 mb-3" v-for="(member, index) in companyGoal?.members"
                                    v-if="companyGoal?.members?.length > 0">
                                    <div class="card-goal-wrapper card shadow-out--sm radius-16 h-100 border-none">
                                        <div class="">
                                            <div class="card-header bg-white radius-16 border-none">
                                                <div class="d-flex justify-content-between">
                                                    <Badge class="align-self-start"
                                                        :class="member?.Status != 0 ? 'green' : 'grey'">
                                                        {{ member?.Status == 0 ? 'Inactive' :
                        'Active' }}
                                                    </Badge>
                                                    <router-link :to="`/user-profile/${ member?.UserId }/${ member?.Startupid }`">
                                                <Button type="button" class="blue btn-sm outline rounded-md no-edge-radius-md">
                                                    View Profile
                                                </Button>
                                            </router-link>
                                                </div>
                                            </div>
                                            <div class="card-body px-3">
                                                <div class="d-flex justify-content-start align-items-center"
                                                    style="word-break: break-all;">
                                                    <Image
                                                        :src="member?.Photo == null || member?.Photo == '' ? `https://www.w3schools.com/howto/img_avatar.png` : `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${member?.Photo}`"
                                                        height="40" width="40"
                                                        class="img-fluid rounded-lg img-team me-3" />
                                                    <div>
                                                        <Badge class="mb-1"
                                                        :class="member?.isOwner ? 'blue' : 'grey'">
                                                        {{ member?.isOwner ? 'Owner' : 'Member' }}
                                                        </Badge>
                                                        <div class="text-md fs-14 profile-name">{{ member?.Email }}
                                                        </div>
                                                        <small class="base-grey profile-name">{{ member?.Name ? member?.Name : '' }}</small>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                                <template v-else>
                                    <p class="f-game fs-24 base-grey text-center">No Team Members' data.</p>
                                </template>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </template>

        <template v-if="activeMenu == 'Value Proposition'">
            <div class="bg-white shadow-out--sm py-5 px-5 radius-16 mb-6" v-if="vpcFields.length > 0">
                <h3 class="f-game fs-32 base-grey">Value Proposition</h3>
                <div class="row">
                    <div class="col-3">
                        <div class="bg-white p-3 radius-16 shadow-out--sm" v-if="vpcFields.length > 0"
                            style="overflow-y:scroll;height:100%; max-height: 50vh;">
                            <div class="d-flex align-items-center justify-content-between py-2 px-3 cursor-pointer tab"
                                id="tab" :class="[vpcIndex == vpcCurrentIndex ? 'current' : '']"
                                v-for="(vpcField, vpcIndex) in vpcFields" ref="tabRef" @click="changeTab(vpcIndex)">
                                <div class="flex-grow-1">
                                    <div class="me-auto">
                                        <div class="text-sm-bold fs-14 grey title">{{ vpcField.Name }}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-9">
                        <template v-for="(vpcField, vpcIndex) in vpcFields">
                            <form v-show="vpcIndex == vpcCurrentIndex">
                                <div class="vpc mb-3">
                                    <label class="form-label f-game fs-20 base-grey">Customer Type</label>
                                    <p>{{ vpcFields[vpcIndex].Name }}</p>
                                </div>

                                <splitpanes class="default-theme" horizontal :push-other-panes="false"
                                    style="height: 100vh">
                                    <pane>
                                        <splitpanes :push-other-panes="false">
                                            <pane class="overflow-y-auto px-3" min-size="15">
                                                <div class="base-textarea-autosize">
                                                    <div class="label-textarea">
                                                        <label class="form-label f-game fs-20 base-grey">Customer
                                                            Jobs</label>
                                                    </div>

                                                    <div class="mb-3" v-for="(jobField, id) in customerJobFields"
                                                        :key="id">
                                                        <div class="textarea-autosize-wrapper position-relative">
                                                            <resize-textarea
                                                                v-model.lazy="vpcFields[vpcCurrentIndex].CustomerJobs[id]"
                                                                :value="vpcFields[vpcCurrentIndex].CustomerJobs[id]"
                                                                :rows="1" :cols="0" class="form-control" disabled>
                                                            </resize-textarea>
                                                        </div>
                                                    </div>

                                                </div>
                                            </pane>
                                            <pane class="overflow-y-auto px-3" min-size="15">
                                                <splitpanes class="default-theme" horizontal :push-other-panes="false">
                                                    <pane class="overflow-y-auto" min-size="15">
                                                        <div class="base-textarea-autosize vpc">
                                                            <div class="label-textarea">
                                                                <label
                                                                    class="form-label f-game fs-20 base-grey">Customer
                                                                    Needs</label>
                                                            </div>
                                                            <div class="mb-3"
                                                                v-for="(needField, id) in customerNeedFields" :key="id">

                                                                <div
                                                                    class="textarea-autosize-wrapper position-relative">
                                                                    <resize-textarea
                                                                        v-model.lazy="vpcFields[vpcCurrentIndex].CustomerNeeds[id].Name"
                                                                        :value="vpcFields[vpcCurrentIndex].CustomerNeeds[id].Name"
                                                                        :rows="1" :cols="0" class="form-control"
                                                                        disabled>
                                                                    </resize-textarea>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </pane>
                                                    <pane class="overflow-y-auto" min-size="15">
                                                        <div class="base-textarea-autosize vpc">
                                                            <div class="label-textarea">
                                                                <label
                                                                    class="form-label f-game fs-20 base-grey">Customer
                                                                    Wants</label>
                                                            </div>
                                                            <div class="mb-3"
                                                                v-for="(wantField, id) in customerWantFields" :key="id">

                                                                <div
                                                                    class="textarea-autosize-wrapper position-relative">
                                                                    <resize-textarea
                                                                        v-model.lazy="vpcFields[vpcCurrentIndex].CustomerWants[id].Name"
                                                                        :value="vpcFields[vpcCurrentIndex].CustomerWants[id].Name"
                                                                        :rows="1" :cols="0" class="form-control"
                                                                        disabled>
                                                                    </resize-textarea>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </pane>
                                                </splitpanes>
                                            </pane>
                                            <pane class="overflow-y-auto px-3" min-size="15">
                                                <label class="form-label f-game fs-20 base-grey">Product
                                                    Features</label>

                                                <div v-for="(feature, id) in mergedFields" :key="feature.Key"
                                                    class="base-textarea-autosize mb-3">
                                                    <div class="textarea-autosize-wrapper position-relative">
                                                        <resize-textarea
                                                            v-model="vpcFields[vpcCurrentIndex][feature.FieldName][fieldIndex(feature?.customerTypeId, feature.Key)].Features"
                                                            :rows="1" :cols="0" class="form-control" disabled>
                                                        </resize-textarea>
                                                    </div>
                                                </div>

                                            </pane>
                                            <pane class="overflow-y-auto px-3" min-size="15">
                                                <div class="base-textarea-autosize">
                                                    <div class="label-textarea">
                                                        <label class="form-label f-game fs-20 base-grey">Product &
                                                            Service</label>
                                                    </div>

                                                    <div class="mb-3" v-for="(productField, id) in productFields"
                                                        :key="id">
                                                        <div class="textarea-autosize-wrapper position-relative">
                                                            <resize-textarea
                                                                v-model.lazy="vpcFields[vpcCurrentIndex].Products[id]"
                                                                :value="vpcFields[vpcCurrentIndex].Products[id]"
                                                                :rows="1" :cols="0" class="form-control" disabled>
                                                            </resize-textarea>
                                                        </div>
                                                    </div>

                                                </div>
                                            </pane>
                                        </splitpanes>
                                    </pane>
                                </splitpanes>
                            </form>
                        </template>
                    </div>

                </div>
            </div>
            <template v-else>
                <p class="f-game fs-24 base-grey text-center">No VPC Data</p>
            </template>
        </template>

        <template v-if="activeMenu == 'Business Model'">
            <div class="bg-white shadow-out--sm py-5 px-5 radius-16 mb-6" v-if="BMCdata != undefined">
                <h3 class="f-game fs-32 base-grey">Business Model Canvas</h3>
                <form v-on:submit.prevent>
                    <splitpanes class="default-theme" horizontal :push-other-panes="false" style="height: 100vh">
                        <pane>
                            <splitpanes :push-other-panes="false">
                                <pane id="libraryBackgroundColor" class="overflow-y-auto px-3" min-size="15">
                                    <BaseTextareaAutosize name="key_partners_column" label="Key Partners" :value="[]"
                                        class="label-fixed" is-disabled />
                                </pane>
                                <pane min-size="15">
                                    <splitpanes horizontal :push-other-panes="false">
                                        <pane class="overflow-y-auto px-3">
                                            <BaseTextareaAutosize name="key_activities_column" label="Key Activities"
                                                :value="[]" class="label-fixed" is-disabled />
                                        </pane>
                                        <pane class="overflow-y-auto px-3">
                                            <BaseTextareaAutosize name="key_resources_column" label="Key Resources"
                                                :value="[]" class="label-fixed" is-disabled />
                                        </pane>
                                    </splitpanes>
                                </pane>
                                <pane class="overflow-y-auto px-3" min-size="15">
                                    <BaseTextareaAutosize name="value_propostion_column" label="Value Proposition"
                                        :value="[]" class="label-fixed" is-disabled />
                                </pane>
                                <pane min-size="15">
                                    <splitpanes horizontal :push-other-panes="false">
                                        <pane class="overflow-y-auto px-3">
                                            <BaseTextareaAutosize name="customer_relationships_column"
                                                label="Customer Relationships" :value="[]" class="label-fixed"
                                                is-disabled />
                                        </pane>
                                        <pane class="overflow-y-auto px-3">
                                            <BaseTextareaAutosize name="channels_column" label="Channels" :value="[]"
                                                class="label-fixed" is-disabled />
                                        </pane>
                                    </splitpanes>
                                </pane>
                                <pane class="overflow-y-auto px-3" min-size="15">
                                    <BaseTextareaAutosize name="customer_segments_column" label="Customer Segments"
                                        :value="[]" class="label-fixed" is-disabled />
                                </pane>
                            </splitpanes>
                        </pane>
                        <pane>
                            <splitpanes :push-other-panes="false">
                                <pane class="overflow-y-auto px-3" min-size="15">
                                    <BaseTextareaAutosize name="cost_structure_column" label="Cost Structure"
                                        :value="[]" class="label-fixed" is-disabled />
                                </pane>
                                <pane class="overflow-y-auto px-3" min-size="15">
                                    <BaseTextareaAutosize name="revenue_streams_column" label="Revenue Streams"
                                        :value="[]" class="label-fixed" is-disabled />
                                </pane>
                            </splitpanes>
                        </pane>
                    </splitpanes>
                </form>
            </div>
            <template v-else>
                <p class="f-game fs-24 base-grey text-center">No BMC Data</p>
            </template>
        </template>
        <template v-if="activeMenu == 'Note'">
            <div class="container my-6">
                <div class="bg-white shadow-out--sm py-5 px-5 radius-16">
                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <h3 class="f-game fs-32 base-grey mb-0">Notes</h3>
                        <Button type="button" class="green btn-sm" @click="onSaveNote" :loading="noteSaving">
                            Save Note
                        </Button>
                    </div>
                    <textarea
                        v-model="startupNote"
                        class="form-control shadow-in--sm radius-16 fs-14"
                        rows="12"
                        placeholder="Write your notes about this startup here..."
                        style="resize: vertical; border: none; outline: none;"
                    ></textarea>
                </div>
            </div>
        </template>
    </div>
</template>

<script setup>
import PageExplanation from "@/components/PageExplanation.vue"
import Badge from "@/components/Badge.vue"
import Image from "@/components/Image.vue"
import Button from "@/components/Button.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import Ionicon from "@/components/Ionicon.vue";
import BaseTextareaAutosize from "@/components/Form/BaseTextareaAutosize.vue"
import BasePickSlider from "@/components/Form/BasePickSlider.vue"
import { useForm } from 'vee-validate';
import { computed, onMounted, ref, defineAsyncComponent, reactive } from "vue"
import { useRoute, useRouter } from "vue-router"
import { storeToRefs } from 'pinia';
import { useCompanyGoalsStore } from "@/stores/companyGoalsStore"
import { adminAuthStore } from "@/stores/adminAuthStore"
import { Splitpanes, Pane } from 'splitpanes'
import 'splitpanes/dist/splitpanes.css'
import { LineChart, BarChart } from 'vue-chart-3';
import { Chart, registerables } from "chart.js";
import { useGoalsStore } from "@/stores/goalsStore"
import Swal from 'sweetalert2'
import { useDebounceFn } from '@vueuse/core';
import slider from "vue3-slider"

Chart.register(...registerables);
const { companyGoal, personalGraph } = storeToRefs(useCompanyGoalsStore());
const { startupInfo, adminAuth } = storeToRefs(adminAuthStore());
const { fetchStartupInfo } = adminAuthStore();
const { fetchCompanyGoal, fetchBMC, fetchVPC, fetchPersonalGraph, fetchNote, saveNote } = useCompanyGoalsStore();
const { startupNote } = storeToRefs(useCompanyGoalsStore());
const { ScoreCorrection } = useGoalsStore();
const route = useRoute();
const router = useRouter();

let rankObj = reactive({
    rank: 0,
    totalCompanies: 0,
    score: 0
});

const Toast = Swal.mixin({
    toast: true,
    position: 'top',
    showConfirmButton: false,
    timer: 5000,
    timerProgressBar: true,
    showCloseButton: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

const readOnlyFields = {
    2: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/Text.vue')),
    3: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/Text.vue')),
    4: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/List.vue')),
    5: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/Text.vue')),
    6: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/DateList.vue')),
    7: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/Text.vue')),
    8: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/FileList.vue')),
    9: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/List.vue')),
    10: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/Text.vue')),
    11: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/Text.vue')),
    12: defineAsyncComponent(() => import('@/components/Form/ReadonlyFields/Text.vue')),
};

const categoryName = {
    1: "Team",
    2: "Company",
    3: "Service",
    4: "Growth"
};

const visibleMenu = reactive({
    0: {
        name: "Quests",
        icon: "ribbon-outline",
        active: true
    },
    1: {
        name: "Teams",
        icon: "people-outline",
        active: false
    },
    2: {
        name: "Value Proposition",
        icon: "layers-outline",
        active: false
    },
    3: {
        name: "Business Model",
        icon: "easel-outline",
        active: false
    },
    4: {
        name: "Note",
        icon: "document-text-outline",
        active: false
    }
});

const activeMenu = computed(() => {
    for (let i in visibleMenu) {
        if (visibleMenu[i].active) {
            return visibleMenu[i].name;
        }
    }
});

const noteSaving = ref(false);

const menuClicked = (index) => {
    for (let i in visibleMenu) {
        visibleMenu[i].active = false;
    }
    visibleMenu[index].active = true;
    if (visibleMenu[index].name === 'Note') {
        fetchNote(route.params.id);
    }
};

const onSaveNote = async () => {
    try {
        noteSaving.value = true;
        await saveNote(route.params.id, startupNote.value);
        Toast.fire({
            icon: 'success',
            title: 'Note saved successfully',
            background: '#ACE7A0',
            iconColor: '#5BBA47',
            color: '#258212'
        });
    } catch (e) {
        Toast.fire({ icon: 'error', title: 'Failed to save note' });
    } finally {
        noteSaving.value = false;
    }
};

let chartData = reactive({
    labels: ["Thursday", "Friday", "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday"],
    datasets: [
        {
            data: [0, 0, 0, 0, 0, 0, 0],
            fill: false,
            borderColor: '#0F934C',
            borderWidth: 4,
            tension: 0.4,
        }
    ],
});

const options = {
    scales: {
        yAxis: {
            // grid: {
            //     display: false
            // },
            // display: false
            title: {
                display: true,
                text: 'Steps done',
            },
        },
    },
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
        tooltip: {
            displayColors: false,
            callbacks: {
                label: function (context) {
                    return context.formattedValue + ' steps done';
                },
            },
            backgroundColor: "#416094",
            titleColor: "#fff",
            titleFont: {
                family: "'Poppins', sans-serif",
                size: 12,
                weight: '500'
            },
            bodyColor: "#fff",
            bodyFont: {
                family: "'Poppins', sans-serif",
                size: 12,
                weight: '500'
            },
            footerColor: "#fff",
            textDirection: 'ltr',
        },
        legend: {
            display: false
        }
    },
};

const selectedGoal = ref(0);
const selectedGoalStep = ref(1);
const isGoalStepsMenuClicked = ref(false);

const selectedGoalObj = computed(() => {
    return companyGoal.value?.companyData[selectedGoal.value];
});

const selectedGoalIndex = computed(() => {
    return companyGoal.value?.companyData.indexOf(companyGoal.value?.companyData[selectedGoal.value]);
});

const selectedGoalStepIndex = computed(() => {
    return companyGoal.value?.companyData[selectedGoal.value]?.steps.indexOf(companyGoal.value?.companyData[selectedGoal.value].steps[selectedGoalStep.value]);
});

const selectedGoalStepObj = computed(() => {
    return companyGoal.value?.companyData[selectedGoal.value]?.steps[selectedGoalStep.value];
});

const selectedGoalStepLength = computed(() => {
    return companyGoal.value?.companyData[selectedGoal.value]?.steps?.length;
});

const selectedGoalCardFieldsObj = computed(() => {
    return companyGoal.value?.companyData[selectedGoal.value]?.steps[selectedGoalStep.value]?.cards;
});

const selectGoal = (idx) => {
    selectedGoal.value = idx;
    selectedGoalStep.value = 0;
    selectedGoalStep.value = 1;
};

const goalStepsMenuClicked = () => {
    isGoalStepsMenuClicked.value = !isGoalStepsMenuClicked.value;
};
const scoreBarShow = ref(true);
const selectGoalStep = (idx) => {
    selectedGoalStep.value = idx;
    scoreBarShow.value = false;
    setTimeout(() => {
        scoreBarShow.value = true;
    }, 500); 
}
const BMCdata = ref();
let vpcData = ref([]);
let vpcFields = ref([]);
let vpcTitle = ref('');
let vpcCurrentIndex = ref(0);
const tabRef = ref();
const highlight = reactive({
    isHighlighted: false,
    index: null,
    customerType: null
});
const onLoading = ref(false);


const onUpdateScore = useDebounceFn(async (event, card) => {
    try {
        onLoading.value = true;
        await ScoreCorrection(card.id, route.params.id, event);
        await fetchCompanyGoal(route.params.id, adminAuth.value.programId);
        Toast.fire({
            icon: 'success',
            title: 'The score was updated successfully',
            background: '#ACE7A0',
            iconColor: '#5BBA47',
            color: '#258212'
        });
        onLoading.value = false;
    } catch (error) {
        onLoading.value = false;
        console.error('Error updating score:', error);
    }
}, 500)


const { handleSubmit, setFieldValue, values, errors, setValues, resetForm } = useForm();

const timePeriodName = ref("Last 7 Days");
const timePeriod = [
    {
        "label": "Last 7 Days",
        "value": "weekly"
    },
    {
        "label": "Last 30 Days",
        "value": "monthly"
    },
    {
        "label": "Last 3 Months",
        "value": "last3months"
    },
    {
        "label": "Last 12 Months",
        "value": "yearly"
    }

];

let timePeriodSelected = ref("weekly");
const onTimePeriodSelect = async (selected) => {
    timePeriodSelected.value = selected;
    await fetchPersonalGraph(adminAuth.value?.programId, route.params.id, timePeriodSelected.value);

    chartData.labels = personalGraph.value?.labels;
    chartData.datasets[0].data = personalGraph.value?.stepPerPeriod;

    timePeriodName.value = timePeriod.find((time) => time.value === selected).label;
}

onMounted(async () => {
    let tooltips = document.querySelectorAll(".tooltip.show");
    tooltips.forEach(tooltip => {
        tooltip.style.display = 'none';
    });

    rankObj.rank = route.query.ranking;
    rankObj.totalCompanies = route.query.total;

    await fetchPersonalGraph(adminAuth.value?.programId, route.params.id, timePeriodSelected.value);
    await fetchCompanyGoal(route.params.id, adminAuth.value.programId);
    await fetchStartupInfo(route.params.id);

    if (route.hash === '#member') {
        visibleMenu[0].active = false;
        visibleMenu[1].active = true;
        document.getElementById("menu")?.scrollIntoView();
    } 

    // await fetchBMC(route.params.id).then((res) => {
    //     if (res.status == 200) {
    //         BMCdata.value = res.data;
    //         setValues({
    //             "key_partners_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.key_partners_column) : [],
    //             "key_activities_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.key_activities_column) : [],
    //             "key_resources_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.key_resources_column) : [],
    //             "value_propostion_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.value_propostion_column) : [],
    //             "customer_relationships_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.customer_relationships_column) : [],
    //             "channels_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.channels_column) : [],
    //             "customer_segments_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.customer_segments_column) : [],
    //             "cost_structure_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.cost_structure_column) : [],
    //             "revenue_streams_column": BMCdata.value != undefined ? JSON.parse(BMCdata.value?.revenue_streams_column) : []
    //         });
    //     }
    // });

    await fetchBMC(route.params.id).then((res) => {
    if (res.status == 200) {
        BMCdata.value = res.data;
        setValues({
            "key_partners_column": BMCdata.value && BMCdata.value.key_partners_column ? JSON.parse(BMCdata.value.key_partners_column) : [],
            "key_activities_column": BMCdata.value && BMCdata.value.key_activities_column ? JSON.parse(BMCdata.value.key_activities_column) : [],
            "key_resources_column": BMCdata.value && BMCdata.value.key_resources_column ? JSON.parse(BMCdata.value.key_resources_column) : [],
            "value_propostion_column": BMCdata.value && BMCdata.value.value_propostion_column ? JSON.parse(BMCdata.value.value_propostion_column) : [],
            "customer_relationships_column": BMCdata.value && BMCdata.value.customer_relationships_column ? JSON.parse(BMCdata.value.customer_relationships_column) : [],
            "channels_column": BMCdata.value && BMCdata.value.channels_column ? JSON.parse(BMCdata.value.channels_column) : [],
            "customer_segments_column": BMCdata.value && BMCdata.value.customer_segments_column ? JSON.parse(BMCdata.value.customer_segments_column) : [],
            "cost_structure_column": BMCdata.value && BMCdata.value.cost_structure_column ? JSON.parse(BMCdata.value.cost_structure_column) : [],
            "revenue_streams_column": BMCdata.value && BMCdata.value.revenue_streams_column ? JSON.parse(BMCdata.value.revenue_streams_column) : []
            });
        }
    });

    await fetchVPC(route.params.id).then((res) => {
        if (!res.data?.hasVPC) {

            let CustomerJobs = res.data?.data.CustomerJobs.replace(/\[|\]|\r|\n|"/g, '').split(',');
            let CustomerNeeds = res.data?.data.CustomerNeeds.replace(/\[|\]|\r|\n|"/g, '').split(',');
            let CustomerWants = res.data?.data.CustomerWants.replace(/\[|\]|\r|\n|"/g, '').split(',');

            let CustomerNeedsEdited = CustomerNeeds?.map((need) => {
                return {
                    "Name": need,
                    "customerTypeId": 0,
                    "Features": "",
                    "HasFeature": false,
                    "Key": crypto.getRandomValues(new Uint32Array(1))[0],
                    "FieldName": "CustomerNeeds"
                }
            });

            let CustomerWantsEdited = CustomerWants?.map((want) => {
                return {
                    "Name": want,
                    "customerTypeId": 1,
                    "Features": "",
                    "HasFeature": false,
                    "Key": crypto.getRandomValues(new Uint32Array(1))[0],
                    "FieldName": "CustomerWants"
                }
            });

            vpcData.value.push({
                "Name": "",
                "CustomerJobs": CustomerJobs,
                "CustomerNeeds": CustomerNeedsEdited,
                "CustomerWants": CustomerWantsEdited,
                "Products": []
            });

            vpcFields.value = vpcData.value;

        } else {
            let extractedData = JSON.parse(res.data.Data);
            vpcFields.value = extractedData;
        }
    });

    chartData.labels = personalGraph.value?.labels;
    chartData.datasets[0].data = personalGraph.value?.stepPerPeriod;

});

const customerJobFields = computed(() => {
    if (vpcFields.value?.length > 0) {
        return vpcFields.value[vpcCurrentIndex.value].CustomerJobs
    } else {
        return [];
    }
});

const customerNeedFields = computed(() => {
    if (vpcFields.value?.length > 0) {
        return vpcFields.value[vpcCurrentIndex.value].CustomerNeeds
    } else {
        return [];
    }
});

const customerWantFields = computed(() => {
    if (vpcFields.value?.length > 0) {
        return vpcFields.value[vpcCurrentIndex.value].CustomerWants
    } else {
        return [];
    }
});

const productFields = computed(() => {
    if (vpcFields.value?.length > 0) {
        return vpcFields.value[vpcCurrentIndex.value].Products
    } else {
        return [];
    }
});

const mergedFields = computed(() => {
    let mergedData = [...customerNeedFields.value, ...customerWantFields.value];
    if (mergedData.length > 0) {
        let data = mergedData.filter((data) => {
            return data?.HasFeature == true;
        });
        return data.sort(
            (objA, objB) => Number(new Date(objA?.Date)) - Number(new Date(objB?.Date)),
        );
    } else {
        return [];
    }

});

const changeTab = (id) => {
    if (tabRef.value[id]?.id == "tab") {
        vpcCurrentIndex.value = id;
    }
}

const fieldIndex = (id_field_type, key) => {
    if (id_field_type == 0) {
        const index = customerNeedFields.value.findIndex(need => {
            return need.Key == key;
        });
        return index;
    }
    if (id_field_type == 1) {
        const index = customerWantFields.value.findIndex(want => {
            return want.Key == key;
        });
        return index;
    }
}


</script>

<style scoped>
.company-goal-wrapper {
    background-repeat: no-repeat;
    background-attachment: scroll;
    background-position: center;
    height: auto;
    min-height: 500px;
    padding: 4rem;
    background-size: cover;
}

.sticky-step-left {
    top: 144px;
    z-index: 9;
}

.current {
    background: #0F934C;
    box-shadow: 0 4px 8px #0f934c59;
    border-radius: 1rem;
}

.current .arrow-btn .arrow-icon {
    color: #FFE3B0 !important;
}

.overflow-y-auto {
    overflow-y: auto;
}

.menu-company .active {
    background-color: #416094;
    box-shadow: 0px 4px 8px rgba(65, 96, 148, 0.35);
    border-radius: 1rem;
}

.p-splitter {
    border: none;
}

.current {
    background: #0F934C;
    box-shadow: 0 4px 8px #0f934c59;
    border-radius: 1rem;
}

.current .title {
    color: #fff;
}

.current .step-number {
    background: #fff;
    border: 1px solid #DFAE55;
    color: #C69030;
    font-weight: 900;
}

.title {
    max-height: 48px;
    line-height: 1.5rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}

.tab {
    height: 64px;
}

.base-textarea-autosize .textarea-autosize-wrapper {
    -webkit-border-radius: 20px;
    -moz-border-radius: 20px;
    border-radius: 1rem;
    border: none;
    color: #113A62;
    font-weight: 500;
    font-size: 14px;
}

.base-textarea-autosize .textarea-autosize-wrapper textarea {
    -webkit-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    -moz-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    border-radius: 1rem;
}

.base-textarea-autosize textarea {
    padding: 1.5rem !important;
    border: none;
    background: #fff;
    font-size: 14px;
}

.base-textarea-autosize.vpc textarea {
    padding: 2rem 1.5rem !important;
}

/* .base-textarea-autosize textarea.form-control:focus {
    box-shadow: none;
} */
.btn.btn-add-text {
    background: #FFFFFF;
    box-shadow: 0px 4px 12px -4px rgb(22 34 51 / 12%), 0px 16px 32px rgb(65 96 148 / 16%);
    border-radius: 16px;
    color: #8B9EB0;
    font-weight: 500;
    font-size: 14px;
    min-height: 50px;
    height: 100%;
    padding: 0.5rem 1.25rem;
}

.base-textarea-autosize .textarea-autosize-wrapper .remove-btn {
    right: 8px;
    top: 8px;
}

.base-textarea-autosize.label-fixed .label-textarea {
    position: sticky;
    top: 0;
    z-index: 1;
    background-color: #f2f2f2;
}

.vpc-feature-btn {
    left: 12px;
    top: 8px;
    font-size: 12px;
    background: #0F934C;
    box-shadow: 0 4px 8px #0f934c59;
    border-radius: 0.5rem;
    padding: 0 0.5rem;
    color: #fff;
    cursor: pointer;
}

.vpc-feature-btn2 {
    left: 12px;
    top: 8px;
    font-size: 12px;
    background-color: #416094;
    box-shadow: 0px 4px 8px rgba(65, 96, 148, 0.35);
    border-radius: 0.5rem;
    padding: 0 0.5rem;
    color: #fff;
    cursor: pointer;
}

.highlighted {
    border: solid 2px #077E3E !important;
    box-shadow: inset -4px -4px 9px rgb(255 255 255 / 88%), inset 4px 4px 14px rgba(7, 126, 62, 21%);
}

.highlighted2 {
    border: solid 2px #416094 !important;
    box-shadow: inset -4px -4px 9px rgb(255 255 255 / 88%), inset 4px 4px 14px rgba(65, 96, 148, 21%);
}
</style>
