<template>
    <div class="tsb-video">
        <div class="ytvideo"
            onclick="thevid=document.getElementById('thevideo'); thevid.style.display='block'; this.style.display='none'">
            <div class="img-gradient">
                <img class="thumb img-fluid" style="cursor: pointer;" :src="props.thumbnail_link" :style="{ 
                    height: props.height,
                    width: props.width 
                }" />
            </div>
            <span class="play-img"></span>
        </div>
        <div id="thevideo" style="display: none;" class="ratio ratio-16x9">
            <iframe :src="props.video_link" title="Youtube Video" frameborder="0" allow="picture-in-picture"
                allowfullscreen></iframe>
        </div>
    </div>
</template>

<script setup>
const props = defineProps({
    thumbnail_link: {
        type: String
    },
    video_link: {
        type: String
    },
    height: {
        type: String
    },
    width: {
        type: String
    }
});
</script>

<style scoped>
.play-img {
    background-position: top left;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    pointer-events: none;
    cursor: pointer;
    width: 20%;
    height: 20%;
    position: absolute;
    top: 50%;
    left: 50%;
    z-index: 99;
    transform: translate(-50%, -50%);
    -webkit-transform: translate(-50%, -50%);
    background-image: url('/play.svg');
}

.ytvideo {
    position: relative;
    height: 100%;
}

.tsb-video.rounded-sm .ytvideo img,
.tsb-video.rounded-sm iframe,
.tsb-video.rounded-sm .img-gradient:after {
    border-radius: 1rem;
}


.tsb-video.rounded-md .ytvideo img,
.tsb-video.rounded-md iframe,
.tsb-video.rounded-md .img-gradient:after {
    border-radius: 2rem;
}

.img-gradient {
    position: relative;
}

.img-gradient:after {
    cursor: pointer;
    content: '';
    position: absolute;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    display: inline-block;
    background-image: -webkit-gradient(linear, left top, left bottom, from(rgba(255, 255, 255, 0)), to(rgba(0, 0, 0, 0.85)));
    background-image: -webkit-linear-gradient(top, rgba(255, 255, 255, 0) 0%, rgba(0, 0, 0, 0.85) 100%);
    background-image: -moz-linear-gradient(top, rgba(255, 255, 255, 0) 0%, rgba(0, 0, 0, 0.85) 100%);
    background-image: -o-linear-gradient(top, rgba(255, 255, 255, 0) 0%, rgba(0, 0, 0, 0.85) 100%);
    background-image: linear-gradient(180deg, rgba(255, 255, 255, 0) 0%, rgba(0, 0, 0, 0.85) 100%);
}

.img-gradient img {
    display: block;
}
</style>