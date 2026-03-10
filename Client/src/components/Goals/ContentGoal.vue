<template>
    <div class="article-wrapper bg-white radius-8 py-2 px-3 shadow--sm mb-c-2 cursor-pointer" v-for="(article, index) in articles"
        :key="index">
        <div class="d-flex justify-content-start align-items-center py-3 px-c-2">
            <div>
                <Badge class="green mb-3">{{ article.article_category }}</Badge>
                <div class="d-flex">
                    <div class="flex-shrink-0">
                        <Image height="140" width="160" :src="article.article_tumbnail" class="rounded-md" />
                    </div>
                    <div class="flex-grow-1 mx-4">
                        <div class="title text-sm-bold fs-16">{{ article.article_title }}</div>
                        <div class="info fs-14 my-2 grey">{{ article.creator }} | {{ article.date_created }}</div>
                        <div class="content fs-14">{{ article.article_description }}</div>
                    </div>
                </div>
            </div>
            <div>
                <Ionicon icon_name="chevron-forward" font_size="48" class="grey-light-1" />
            </div>
        </div>
    </div>
</template>

<script setup>
import Button from "@/components/Button.vue"
import Ionicon from "@/components/Ionicon.vue";
import Image from "@/components/Image.vue"
import Badge from "@/components/Badge.vue"

const articles = [
    {
        "id": 1,
        "article_title": "Article 1",
        "required": true,
        "locked_status": true,
        "creator": "Creator 1",
        "date_created": "2022-09-01T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should questions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you shouldquestions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Upgrade to unlock"
    },
    {
        "id": 2,
        "article_title": "Article 2",
        "required": true,
        "locked_status": false,
        "creator": "Creator 2",
        "date_created": "2022-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 3,
        "article_title": "Article 3",
        "required": true,
        "locked_status": false,
        "creator": "Creator 1",
        "date_created": "2022-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 4,
        "article_title": "Article 4",
        "required": true,
        "locked_status": false,
        "creator": "Creator 2",
        "date_created": "2022-09-03T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 5,
        "article_title": "Article 5",
        "required": true,
        "locked_status": true,
        "creator": "Creator 3",
        "date_created": "2022-09-05T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Upgrade to unlock"
    },
    {
        "id": 6,
        "article_title": "Article 6",
        "required": true,
        "locked_status": true,
        "creator": "Creator 1",
        "date_created": "2022-08-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Upgrade to unlock"
    },
    {
        "id": 7,
        "article_title": "Article 7",
        "required": true,
        "locked_status": true,
        "creator": "Creator 3",
        "date_created": "2022-07-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Upgrade to unlock"
    },
    {
        "id": 8,
        "article_title": "Article 8",
        "required": true,
        "locked_status": true,
        "creator": "Creator 1",
        "date_created": "2021-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Upgrade to unlock"
    },
    {
        "id": 9,
        "article_title": "Article 9",
        "required": true,
        "locked_status": true,
        "creator": "Creator 3",
        "date_created": "2022-08-05T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Upgrade to unlock"
    },
    {
        "id": 10,
        "article_title": "Article 10",
        "required": true,
        "locked_status": false,
        "creator": "Creator 1",
        "date_created": "2022-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 11,
        "article_title": "Article 11",
        "required": true,
        "locked_status": false,
        "creator": "Creator 2",
        "date_created": "2022-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 12,
        "article_title": "Article 12",
        "required": true,
        "locked_status": false,
        "creator": "Creator 1",
        "date_created": "2022-08-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 13,
        "article_title": "Article 13",
        "required": true,
        "locked_status": false,
        "creator": "Creator 4",
        "date_created": "2022-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 14,
        "article_title": "Article 14",
        "required": true,
        "locked_status": true,
        "creator": "Creator 1",
        "date_created": "2022-08-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Upgrade to unlock"
    },
    {
        "id": 15,
        "article_title": "Article 15",
        "required": true,
        "locked_status": false,
        "creator": "Creator 1",
        "date_created": "2022-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    },
    {
        "id": 16,
        "article_title": "Article 16",
        "required": true,
        "locked_status": false,
        "creator": "Creator 2",
        "date_created": "2020-09-02T00:00:00.000Z",
        "article_description": "What makes a startup different from a \"normal\" company, what are the 5 questions you should be asking yourself every day when building your business and how does startup fundraising work.",
        "article_tumbnail": "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/event-img-prod/17e8e7c7-5e80-47ca-809b-265b01650fe5AWS.jpg",
        "article_category": "Fundraising"
    }
]


</script>

<style scoped>
</style>