<template>
    <PageExplanation
    :logo="adminAuth?.photo"
    :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`"
    >
        <template v-slot:top-content>
            <div class="d-flex justify-content-end align-items-center">

                <BaseCircleButton class="blue" width="48" height="48" :class="{
                    'div-disabled': isButtonClicked,
                    'button--loading': isButtonClicked
                }" @click="createGoal"
                    data-bs-toggle="tooltip" data-bs-placement="left"
                    data-bs-title="Create a quest for startups"
                >
                    <div class="d-flex flex-column justify-content-center align-items-center">
                        <Ionicon icon_name="add-outline" font_size="16" />
                        <div class="fs-12 f-game">Create</div>
                    </div>
                </BaseCircleButton>

            </div>
        </template>

        <template v-slot:body-content>
            <div class="row justify-content-between align-items-start content-inside user-select-none py-3">
                <div class="col-5">
                    <div class="d-flex align-content-center align-items-center">
                        <Image src="/alien.svg" height="80" width="56" />
                        <div>
                            <div class="shadow-out--sm bg-white ms-3 p-3 radius-16 custom-dialog-box">
                                <span class="fs-16 text-sm-bold text-green">Hello {{ adminAuth.name }},</span><br />
                                <span class="fs-14 grey">
                                    Welcome! I am The Startup Buddy and I will guide you through your startup journey.
                                    Get
                                    started by working on your first quest :)</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="shadow-out--sm radius-16 d-flex column row">
                        <div class="d-flex px-4 align-items-center my-c-2">
                            <div class="flex-shrink-0">
                                <div class="shadow-out--sm p-3 rounded-md">
                                    <Ionicon icon_name="calendar-clear-outline" font_size="24" class="grey" />
                                </div>
                            </div>
                            <div class="flex-grow-1 ms-3">
                                <div class="f-game fs-24 text-md text-info">PROGRAM ACTIVITY</div>
                                <div class="fs-14 text-info text-md">{{ timePeriodName }}</div>
                            </div>
                        </div>
                        <BarChart :chartData="data" class="chart-wrapper" :options="BarOptions" ref="barChartRef" />
                    </div>
                </div>
                <div class="col-3">
                    <div>
                        <div class="fs-24 text-md text-center text-warning f-game mb-4">Create a Quest</div>

                        <div class="btn-add-layer-2" @click="createGoal" :class="{
                    'div-disabled': isButtonClicked,
                }">
                            <div class="btn-add-layer-1 d-table mx-auto">
                                <BaseCircleButton class="btn-content blue btn-circle-shadow"
                                    :class="{ 'button--loading': isButtonClicked }" width="86" height="86">
                                    <div class="d-flex flex-column justify-content-center align-items-center">
                                        <Ionicon icon_name="add-outline" font_size="24" />
                                        <div class="fs-18 f-game">Create</div>
                                    </div>
                                </BaseCircleButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </template>
    </PageExplanation>

    <div class="container main-wrapper my-6">

        <div class="mb-6">
            <div class="d-flex justify-content-between align-items-start mb-5">
                <div class="d-flex align-items-center">
                    <div class="flex-shrink-0">
                        <div class="shadow-out--sm p-3 rounded-md">
                            <Ionicon icon_name="calendar-clear-outline" font_size="32" class="grey" />
                        </div>
                    </div>
                    <div class="flex-grow-1 ms-3">
                        <div class="d-flex justify-content-start align-items-center gap-3">
                            <div class="f-game fs-24 text-md text-info">PROGRAM ACTIVITY</div>
                            <div class="cursor-pointer" data-bs-toggle="dropdown" aria-expanded="false">
                                <Ionicon icon_name="ellipsis-vertical-circle-sharp" class="grey" font_size="32" />
                            </div>
                            <ul class="dropdown-menu" style="min-width: auto;">
                                <li v-for="(time, index) in timePeriod" @click="onTimePeriodSelect(time.value)"><span
                                        class="fs-14 cursor-pointer dropdown-item">{{ time.label }}</span></li>
                            </ul>
                        </div>
                        <div class="fs-14 text-info text-md">{{ timePeriodName }}</div>
                    </div>
                </div>
                <div class="d-flex justify-content-start">
                    <div class="text-center me-5 shadow-out--sm p-3 rounded-md">
                        <div class="text-bold fs-32 text-md base-grey-1">
                            {{ graph?.totalCompanies }}
                        </div>
                        <div class="fs-14 base-grey-1">
                            Total Companies
                        </div>
                    </div>
                    <div class="text-center me-5">
                        <div class="fs-14 base-grey-1">
                            Steps last period
                        </div>
                        <div class="text-bold fs-16 text-sm-bold base-grey-1">
                            {{ graph?.stepLastTime }}
                        </div>
                    </div>
                    <div class="text-center">
                        <div class="fs-14 base-grey-1">
                            Steps this period
                        </div>
                        <div class="text-bold fs-16 text-sm-bold base-grey-1">
                            {{ graph?.stepThisTime }}
                        </div>
                    </div>
                </div>
            </div>
            <LineChart :chartData="chartData" :options="options" />
        </div>

        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">Control Center</div>
            <div class="fs-14 grey text-sm-bold">Hey There! Here you can manage your program</div>
            <div class="bg-white shadow-out--sm py-3 px-2 radius-16 text-center mt-6 user-select-none cursor-pointer">
                <div class="row">
                    <div class="col-3">
                        <div @click="goToCompanyList">
                            <div class="f-game fs-32 text-info">Companies</div>
                            <Ionicon icon_name="business-outline" class="info p-4" font_size="96" />
                        </div>
                    </div>
                    <div class="col-3">
                        <div @click="goToGoals">
                            <div class="f-game fs-32 text-info">Quests</div>
                            <Ionicon icon_name="ribbon-outline" class="info p-4" font_size="96" />
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="f-game fs-32 text-disabled">Content</div>
                        <Ionicon icon_name="document-text-outline" class="grey-light-1 p-4" font_size="96" />
                        <div class="f-game fs-16 text-disabled">Coming soon</div>
                    </div>
                    <div class="col-3">
                        <div class="f-game fs-32 text-disabled">Users</div>
                        <Ionicon icon_name="person-circle-outline" class="grey-light-1 p-4" font_size="96" />
                        <div class="f-game fs-16 text-disabled">Coming soon</div>
                    </div>
                </div>
            </div>
        </div>

        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">Portfolio</div>
            <Form class="row align-items-center">
                <div class="col-1">
                    <!-- <BaseSelect :options="typeList" name="type_list" label="Select type" placeholder="All Companies" /> -->
                    <BaseSelect :options="[24, 48]" name="show_data" label="Show :" :value="24" ref="showEntries"
                        @onSelect="onSelectShowEntries" />
                </div>
                <div class="col-10">
                    <div class="d-flex justify-content-start align-items-center gap-3">
                        <!-- <BaseSelect name="select_program" type-rule="text" @onSelect="onSelectProgram"
                            :options="programList" :value="currentProgram?.Programid"
                            :placeholder="adminAuth.programId == 0 ? 'ALL' : ''" label="Select program"
                            value-by="Programid" label-by="Name" :empty-model-value="''"
                            v-if="adminAuth.role == 'Configuration Access'" style="width: 300px;" /> -->

                        <BaseSelect :options="programGroupList" name="program_group_id" label="Program Group" class=""
                            v-if="programGroup?.length > 0" value-by="programGroupId" label-by="groupName"
                            placeholder="Select a group" style="width: 300px;" ref="programGroupRef"
                            :value="programGroupId" @onSelect="onSelectProgramGroup" />
                        <BaseInput name="search_input" label="Search"
                            placeholder="Search for a Startup by name or email" type="text" ref="searchInput"
                            @input="onInputSearch" class="w-100" />
                    </div>
                </div>
                <div class="col-1">
                    <div class="btn-group">
                        <Button type="button" class="green btn-sm rounded-md outline no-edge-radius-md"
                            data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                            <Ionicon icon_name="ellipsis-vertical-outline" font_size="24" />
                        </Button>
                        <ul class="dropdown-menu dropdown-menu-lg-end">
                            <li>
                                <BaseInputSwitch name="is_big_card" label="Details" ref="toggleRef"
                                    @onSwitchChange="bigCards" :value="isBigCards" />
                            </li>
                            <li>
                                <BaseInputSwitch name="sort_by_coins" label="Sort by coins" ref="toggleCoinsRef"
                                    @onSwitchChange="onToggleCoins" />
                            </li>
                        </ul>
                    </div>
                </div>
            </Form>
        </div>

        <div id="company-list">

            <template v-if="!isBigCards">
                <div class="row" v-if="listCompanies?.companies?.length > 0 && listCompanies?.companies != undefined">
                    <div class="col-3" v-for="(list, index) in listCompanies?.companies" :key="index">
                        <div class="bg-white shadow-out--sm py-2 px-3 radius-16 mb-5">
                            <div class="d-flex justify-content-start align-items-center">
                                <Image
                                    :src="list.startupLogo != null ? `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${list.startupLogo}` : `https://cdn.shoplightspeed.com/shops/617794/themes/668/assets/cat-icon.png?20190516145812`"
                                    height="48" width="48" class="img-fluid rounded-lg" />
                                <router-link
                                    :to="`/company-profile/${list.startupid}`"
                                    style="text-decoration: none;">
                                    <div class="ms-3 text-sm-bold fs-14 company-name">{{ list.companyname }}</div>
                                </router-link>
                            </div>
                            <div class="my-3">
                                <p class="fs-14 company-description">{{ list.description }}</p>
                            </div>
                            <div class="d-flex justify-content-center align-items-center">
                                <div
                                    class="shadow-in--sm radius-16 d-flex justify-content-between px-2 align-items-center point-info w-50 me-2">
                                    <div class="text-green fs-14"><span class="text-bold">{{ list.ranking }}</span><span
                                            class="base-grey fs-14">/ {{ listCompanies?.TotalPage }}</span></div>
                                    <Image src="/trophy.svg" height="16" width="16"></Image>
                                </div>
                                <router-link :to="`/coins-history/${list.startupid}`" class="text-decoration-none">
                                <div
                                    class="shadow-in--sm radius-16 d-flex justify-content-between px-2 align-items-center point-info w-50 ms-2">
                                    <div class="text-warning fs-14 text-bold">{{ list.tsbCoin.toLocaleString("en-US") }}
                                    </div>
                                    <Image src="/coin.svg" height="16" width="16"></Image>
                                </div>
                            </router-link>
                            </div>
                        </div>
                    </div>
                </div>

                <template v-else-if="listCompanies?.companies?.length == 0 && listCompanies?.TotalPage == 0">
                    <p class="f-game fs-24 base-grey text-center">Oops! The query was not found. How about trying a
                        different keyword?</p>
                </template>

                <div class="row" v-else>
                    <div class="col-3" v-for="(n, index) in 24" :key="index">
                        <div class="bg-white shadow-out--sm py-2 px-3 radius-16 mb-5">
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="placeholder-wave w-20">
                                    <span class="placeholder placeholder-lg col-12 bg-secondary rounded-lg"
                                        style="width: 48px;height: 48px;"></span>
                                </div>
                                <div class="placeholder-wave w-80 ms-2">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                            </div>
                            <div class="my-3">
                                <div class="placeholder-wave">
                                    <span class="placeholder placeholder-lg col-12 bg-secondary radius-8"></span>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center align-items-center">
                                <div class="placeholder-wave w-50">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                                <div class="placeholder-wave w-50 ms-2">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </template>

            <template v-else>
                <div class="bg-white shadow-out--sm py-2 px-3 radius-16 mb-5"
                    v-for="(list, index) in listCompanies?.companies" :key="list"
                    v-if="listCompanies?.companies?.length > 0 && listCompanies?.companies != undefined">
                    <div class="row mb-3">
                        <div class="col-3">
                            <div class="d-flex align-items-center justify-content-start">
                                <Image
                                    :src="list.startupLogo != null ? `https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${list.startupLogo}` : `https://cdn.shoplightspeed.com/shops/617794/themes/668/assets/cat-icon.png?20190516145812`"
                                    height="48" width="48" class="img-fluid rounded-lg" />
                                <div class="ms-3 text-sm-bold fs-20 text-break">{{ list.companyname }}</div>
                            </div>
                        </div>
                        <div class="col-5">
                            <div class="d-flex align-items-center justify-content-start">
                                <Image
                                    :src="list.level[0] != '' ? list.level[0] : 'https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/personel-icon-0.png'"
                                    width="48" height="48" class="mx-2 of-contain"
                                    :class="{ greyscale: list.level[0].includes('0') }" />
                                <Image
                                    :src="list.level[1] != '' ? list.level[1] : 'https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/company-icon-0.png'"
                                    width="48" height="48" class="mx-2 of-contain"
                                    :class="{ greyscale: list.level[1].includes('0') }" />
                                <Image
                                    :src="list.level[2] != '' ? list.level[2] : 'https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/concept-icon-0.png'"
                                    width="48" height="48" class="mx-2 of-contain"
                                    :class="{ greyscale: list.level[2].includes('0') }" />
                                <Image
                                    :src="list.level[3] != '' ? list.level[3] : 'https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/Assets/growth-icon-0.png'"
                                    width="48" height="48" class="mx-2 of-contain"
                                    :class="{ greyscale: list.level[3].includes('0') }" />
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="d-flex justify-content-end align-items-center">
                                <!-- <Badge class="goal-label green me-3">{{ list.program }}</Badge> -->
                                <!-- <Badge class="goal-label blue me-3">{{ list.companyEmail }}</Badge> -->
                                <div
                                    class="shadow-in--sm radius-16 d-flex justify-content-between px-3 align-items-center point-info mw-200">
                                    <div class="text-green fs-24"><span class="text-bold"> {{ list.ranking
                                            }}</span><span class="base-grey fs-14">/ {{ listCompanies?.TotalPage
                                            }}</span></div>
                                    <Image src="/trophy.svg" height="24" width="24"></Image>
                                </div>
                                <div
                                    class="shadow-in--sm radius-16 d-flex justify-content-between px-3 align-items-center point-info mw-200 ms-3">
                                    <span class="base-grey fs-16">Score: </span>
                                    <span class="base-grey text-bold fs-24"> {{ list.Score
                                        }}</span>
                                </div>
                                <router-link :to="`/coins-history/${list.startupid}`" class="text-decoration-none">
                
                                <div
                                    class="shadow-in--sm radius-16 d-flex justify-content-between px-3 align-items-center point-info mw-200 ms-3">
                                    <div class="text-warning fs-24 text-bold">{{ list.tsbCoin.toLocaleString("en-US") }}
                                    </div>
                                    <Image src="/coin.svg" height="24" width="24"></Image>
                                </div>
                            </router-link>
                            </div>
                        </div>
                    </div>

                    <div class="row d-flex align-items-end">
                        <div class="col-3">
                            <router-link :to="`/company-profile/${ list?.startupid }#member`">
                            <div class="avatar-group justify-content-end ms-2">
                                <div class="avatar-more" v-if="list.memberphoto.length > 5">+{{
                    list.memberphoto.length - 5 }}</div>
                                <template v-for="(value, index) in list.memberphoto.slice(0, 5)" :key="index">
                                    
                                    <Image v-if="value"
                                        :src="`https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/Next-TSB/User-img/${value}`"
                                        alt="DP" class="rounded-lg shadow" width="35" height="35" />
                                    <Image v-else src="https://www.w3schools.com/howto/img_avatar.png" alt="DP"
                                        class="rounded-lg shadow" width="35" height="35" />
                                </template>
                            </div>
                            </router-link>
                        </div>
                        <div class="col-3">
                            <div
                                class="shadow-in--sm radius-16 d-flex justify-content-between px-3 align-items-center point-info w-full">
                                <div class="text-green fs-24 text-bold d-flex align-items-center">
                                    {{ list.completeStep }}
                                    <div class="ms-2 fs-16 grey">/ {{ list.totalSteps }}</div>
                                </div>
                                <div class="ms-2 fs-16 grey f-game">Steps</div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="d-flex justify-content-end align-items-end">
                                <Button class="red outline rounded-md me-3" type="button"
                                    @click="removeStartupProgram(list.startupid)"
                                    :class="{ invisible: list.Programid == 0 }"
                                    :loading="buttonRemoveProgram.loading && buttonRemoveProgram.id == list.startupid"
                                    :disabled="buttonRemoveProgram.disabled && buttonRemoveProgram.id == list.startupid"
                                    data-bs-toggle="tooltip" data-bs-placement="top"
                                    data-bs-title="Remove the program of the startup"
                                    >
                                    <Ionicon icon_name="trash-outline" font_size="24" />
                                </Button>

                                <BaseSelect class="no-label normalize me-3" :name="`program_startup_${list.startupid}`"
                                    type-rule="text"
                                    @onSelect="onSelectStartupProgram(list.startupid, $event, list?.ProgramGroupId)"
                                    :options="programs" placeholder="Select program"
                                    :value="programs.find(x => x.Programid == list.Programid)?.Programid"
                                    value-by="Programid" label-by="Name" :empty-model-value="''"
                                    label="Startup Program"
                                    v-if="adminAuth.role == 'Configuration Access' && programs?.length > 1" 
                                    />

                                <BaseSelect :options="list?.programGroups" :name="`program_group_id_${list?.startupid}`"
                                    label="Startup Program Group" class="no-label normalize me-3"
                                    v-if="list?.programGroups?.length > 1" value-by="ProgramGroupId"
                                    label-by="GroupName" placeholder="Select a group"
                                    :value="list?.programGroups && list.programGroups?.length > 0 ? list.programGroups?.find(x => x.ProgramGroupId == list?.ProgramGroupId)?.ProgramGroupId : 0"
                                    @onSelect="onSelectStartupProgram(list.startupid, list.Programid, $event)" 
                                    />
                                <router-link
                                    :to="`/company-profile/${list.startupid}`"
                                    class="me-3">
                                    <Button class="blue outline rounded-md" type="button" data-bs-toggle="tooltip" data-bs-placement="top"
                                        data-bs-title="Startup detail info">
                                        <Ionicon icon_name="open-outline" font_size="24" />
                                    </Button>
                                </router-link>
                                <a :href="`https://mail.google.com/mail/?view=cm&fs=1&to=${list.companyEmail}&su=&cc=&bcc=&body=`"
                                    target="_blank">
                                    <Button class="blue outline rounded-md" type="button" data-bs-toggle="tooltip" data-bs-placement="top"
                                        data-bs-title="Send an email to the startup">
                                        <Ionicon icon_name="paper-plane-outline" font_size="24" />
                                    </Button>
                                </a>

                            </div>
                        </div>
                    </div>
                </div>

                <template v-else-if="listCompanies?.companies?.length == 0 && listCompanies?.TotalPage == 0">
                    <p class="f-game fs-24 base-grey text-center">Oops! The query was not found. How about trying a
                        different keyword?</p>
                </template>

                <div class="bg-white shadow-out--sm py-2 px-3 radius-16 mb-5" v-for="(n, index) in 10" v-else>
                    <div class="row mb-3">
                        <div class="col-3">
                            <div class="placeholder-wave">
                                <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                            </div>
                        </div>
                        <div class="col-5">
                            <div class="row">
                                <div class="placeholder-wave col-3">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                                <div class="placeholder-wave col-3">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                                <div class="placeholder-wave col-3">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                                <div class="placeholder-wave col-3">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="placeholder-wave">
                                <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-3">
                            <div class="placeholder-wave">
                                <span class="placeholder placeholder-lg col-3 radius-8 bg-secondary"></span>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="placeholder-wave">
                                <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                            </div>
                        </div>
                        <div class="col-5">
                            <div class="row">
                                <div class="placeholder-wave col-6">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                                <div class="placeholder-wave col-6">
                                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </template>

        </div>

        <div class="d-flex justify-content-center">
            <div>
                <VueAwesomePaginate :total-items="totalPage" :items-per-page="itemShowPerPage" :max-pages-shown="10"
                    v-model="currentPage" :on-click="onClickHandler" />
            </div>
        </div>

    </div>

    <div class="container main-wrapper mb-6">

        <div class="mb-6">
            <div class="fs-24 text-sm-bold mb-3">How do quests work?</div>
            <div class="fs-14 grey text-sm-bold">To create a quest, you must assign a level and category to it.
                After that you need to follow the flow where you add Quest details, rewards, steps and fields.
                Publish the quest for startups for them to complete and receive the rewards.</div>
        </div>

        <Accordion :accordion-data="accordionObj" />

    </div>
</template>

<script setup>
import { ref, inject, onMounted, reactive, computed } from "vue";
import { useRouter, useRoute } from "vue-router";
import Ionicon from "@/components/Ionicon.vue";
import Card from "@/components/Card.vue"
import Icon from "@/components/Icon.vue"
import Badge from "@/components/Badge.vue"
import ButtonGame from "@/components/ButtonGame.vue"
import Image from "@/components/Image.vue"
import PageExplanation from "@/components/PageExplanation.vue"
import TextLevelGoal from "@/components/Goals/TextLevelGoal.vue"
import NumberTextCircle from "@/components/Goals/NumberWithCircle.vue"
import Steps from "@/components/Goals/Steps.vue"
import Button from "@/components/Button.vue"
import Accordion from "@/components/Accordion.vue";
import { useGoalsStore } from "@/stores/goalsStore"
import { useListCompanyStore } from "@/stores/listCompanyStore"
import { adminAuthStore } from "@/stores/adminAuthStore"
import { storeToRefs } from 'pinia';
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import { Form, useForm } from 'vee-validate'
import BaseCircleButton from "@/components/Button/BaseCircleButton.vue"
import { LineChart, BarChart } from 'vue-chart-3';
import { Chart, registerables } from "chart.js";
import { useDebounceFn, watchDebounced } from '@vueuse/core'
import { useSignalR } from '@dreamonkey/vue-signalr';

const axios = inject("axios");
const signalr = useSignalR();
const router = useRouter();
const route = useRoute();
const { goalDefault, goalFieldSchema, SaveGoal } = useGoalsStore();
const { listCompanies, graph } = storeToRefs(useListCompanyStore());
const { adminAuth, programList, programs, currentProgram, programGroup, currentProgramGroup } = storeToRefs(adminAuthStore());
const { fetchProgramGroup } = adminAuthStore();
const authStore = adminAuthStore();
const { fetchListCompany, fetchGraph, removeProgram, switchProgram } = useListCompanyStore();
Chart.register(...registerables);

const showEntries = ref();
const searchInput = ref();
const currentPage = ref(1);
const totalPage = ref(1);
const itemShowPerPage = ref();
const barChartRef = ref();
const currentRouteName = computed(() => router.currentRoute.value.name);
const programGroupRef = ref();

const isProgramGroupALL = computed(() => {
    return programGroupRef.value?.selectRef?.selected?.groupName == 'ALL' ? true : false
});

const programGroupIdFunc = computed(() => {
    return programGroupRef.value ? (programGroupRef.value.selectRef.selected.groupName == 'ALL' || programGroupRef.value.selectRef.selected.groupName == 'No Group' ? 0 : programGroupRef.value.selectRef.selected.programGroupId) : 0;
});

let isBigCards = ref(true);
const bigCards = () => {
    isBigCards.value = !isBigCards.value;
}
let sortByRank = ref(false);
const onToggleRank = async (value) => {
    sortByRank.value = value;
    listCompanies.value = [];
    await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value);
}
let sortByCoins = ref(false);
const onToggleCoins = async (value) => {
    sortByCoins.value = value;
    listCompanies.value = [];
    await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value);
}
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
    await fetchGraph(adminAuth.value?.programId, selected);
    chartData.labels = graph.value?.labels;
    chartData.datasets[0].data = graph.value?.stepPerPeriod;

    data.labels = graph.value?.labels;
    data.datasets[0].data = graph.value?.stepPerPeriod;

    timePeriodName.value = timePeriod.find((time) => time.value === selected).label;
}

const programGroupList = computed(() => {
    return [
        {
            programGroupId: 0,
            groupName: "ALL"
        },
        {
            programGroupId: -1,
            groupName: "No Group"
        },
        ...programGroup.value
    ];
});

onMounted(async () => {
    listCompanies.value = [];
    await fetchProgramGroup(adminAuth.value.programId);
    await fetchGraph(adminAuth.value.programId, timePeriodSelected.value);
    await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value);

    chartData.labels = graph.value?.labels;
    chartData.datasets[0].data = graph.value?.stepPerPeriod;
    totalPage.value = listCompanies.value?.TotalPage ?? 1;
    itemShowPerPage.value = showEntries.value?.selectRef.selected;

    data.labels = graph.value?.labels;
    data.datasets[0].data = graph.value?.stepPerPeriod;
});

watchDebounced(
  () => adminAuth.value.programId, 
  async (newValue, oldValue) => {
    listCompanies.value = [];
    await fetchGraph(adminAuth.value.programId, timePeriodSelected.value);
    chartData.labels = graph.value?.labels;
    chartData.datasets[0].data = graph.value?.stepPerPeriod;
    await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, programGroupIdFunc.value, true);
    await fetchProgramGroup(adminAuth.value.programId);
    currentPage.value = 1;
    totalPage.value = listCompanies.value?.TotalPage ?? 1;
    itemShowPerPage.value = showEntries.value?.selectRef.selected;
  },
  0
);

let programGroupId = ref(0);
const onSelectProgramGroup = async (id) => {
    adminAuth.value.programGroupId = id;
    listCompanies.value = [];
    await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value);
    await fetchProgramGroup(adminAuth.value.programId);
    currentPage.value = 1;
    totalPage.value = listCompanies.value?.TotalPage ?? 1;
    itemShowPerPage.value = showEntries.value?.selectRef.selected;
}
const selectedStartupId = ref(0);
// const onSelectedStartupProgram = async (newProgramId) => {
//     selectedStartupId.value = newProgramId;
//     await fetchGraph(adminAuth.value.programId, timePeriodSelected.value);
//     await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, programGroupIdFunc.value, isProgramGroupALL.value);
// }

const onSelectStartupProgram = (startupId, programId, programGroupId) => {
    switchProgram(startupId, programId, programGroupId).then((res) => {
        if (res.status == 200) {
            listCompanies.value = [];
            fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, currentPage.value, adminAuth.value?.programId, programGroupIdFunc.value, isProgramGroupALL.value);
        }
    });
}
const goToGoals = () => {
    return router.push("/quests");
}

const goToCompanyList = () => {
    document.getElementById('company-list').scrollIntoView();
}
const isButtonClicked = ref(false);
const buttonRemoveProgram = reactive({
    loading: false,
    success: false,
    fail: false,
    disabled: false,
    id: 0
});
const createGoal = () => {

    isButtonClicked.value = true;

    let formData = new FormData();

    formData.append('data', JSON.stringify(goalDefault));
    formData.append('data_ui', JSON.stringify(goalFieldSchema));
    formData.append('programId', adminAuth.value.programId);

    SaveGoal(formData).then((res) => {
        if (res.status === 200) {

            return router.push(`/edit-quest/${res.data}`);

        }
    }).then(() => {
        isButtonClicked.value = false;
    });

}
const onSelectShowEntries = async (selected) => {
    listCompanies.value = [];
    await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, selected, 1, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value);
    totalPage.value = listCompanies.value?.TotalPage;
    currentPage.value = 1;
    itemShowPerPage.value = showEntries.value?.selectRef.selected;

}

const onInputSearch = useDebounceFn(async (text) => {
    listCompanies.value = [];
    await fetchListCompany(text.target.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value);
    totalPage.value = listCompanies.value?.TotalPage;
    currentPage.value = 1;
    itemShowPerPage.value = showEntries.value?.selectRef.selected;

}, 500);

const onClickHandler = async (number) => {
    currentPage.value = number;
    listCompanies.value = [];
    await fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, number, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value);
};

const removeStartupProgram = async (startupid) => {
    let text = "Are you sure you want to delete this startup?";
    if (confirm(text) == true) {
        buttonRemoveProgram.loading = true;
        buttonRemoveProgram.disabled = true;
        buttonRemoveProgram.id = startupid;
        await removeProgram(startupid).then((res) => {
            if (res.status == 200) {
                listCompanies.value = [];
                fetchListCompany(searchInput.value?.inputValueRef.value, sortByRank.value, sortByCoins.value, showEntries.value?.selectRef.selected, 1, adminAuth.value.programId, programGroupIdFunc.value, isProgramGroupALL.value).then(() => {
                    buttonRemoveProgram.loading = false;
                    buttonRemoveProgram.disabled = false;
                    buttonRemoveProgram.id = 0;
                });
            }
        }).catch(() => {
            buttonRemoveProgram.loading = false;
            buttonRemoveProgram.disabled = false;
            buttonRemoveProgram.id = 0;
        });
    }
}

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
        'content': 'Click on the “Create” (link to create) button at the top of the screen. You will be redirected to the Quests section. Select a level and category for the quest. Then fill out the quest details and reward you want to provide after completion. Add steps to the quest and publish it so that startups can access and complete them.'
    },
    {
        'title': 'What are levels and categories?',
        'content': 'Each quest can be divided into 3 levels (Lvl 1 - Lvl Max) based on their importance. Also, you can divide the quests into 4 categories (Company, Team, Service and Growth). Example: Important details about companies (valuation, funding etc.) will come under LVL 1 for Company, Team details will come under LVL 1 for Team, etc.).'
    }
]

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

const data = reactive({
    labels: [],
    datasets: [
        {
            backgroundColor: "#0F934C",
            data: [],
            borderRadius: Number.MAX_VALUE,
            barThickness: 8,
        }
    ]
})

const BarOptions = ref({
    scales: {
        xAxis: {
            grid: {
                display: false
            },
            display: false
        },
        yAxis: {
            grid: {
                display: false
            },
            display: false
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
});

</script>

<style scoped>
.company-description {
    max-height: 4rem;
    height: 500px;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}

.company-name {
    max-height: 1.25rem;
    height: 500px;
    /* line-height: 1.5rem; */
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    word-break: break-all;
}
</style>