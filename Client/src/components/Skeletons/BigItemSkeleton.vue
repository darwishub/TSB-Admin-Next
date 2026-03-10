<template>
    <div class="skeleton">
        <div class="card-goal-wrapper card shadow--sm radius-16">
            <div class="card-header bg-white radius-16 shadow--md">
                <div class="row justify-content-between align-items-center">
                    <div class="col-8">
                        <p class="placeholder-wave">
                            <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                        </p>
                    </div>
                    <div class="col-4">
                        <p class="placeholder-wave">
                            <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                        </p>
                    </div>
                </div>

                <p class="placeholder-wave">
                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                </p>
                <div class="placeholder-wave">
                    <div class="placeholder placeholder-lg col-12 radius-8 img-skeleton bg-secondary"></div>
                </div>
            </div>
            <div class="card-body card-img p-0 placeholder-wave">
                <div class="placeholder placeholder-lg col-12 radius-8 img-skeleton bg-secondary"></div>
            </div>
            <div class="card-footer radius-16 shadow--md bg-white">
                <div class="placeholder-wave">
                    <div class="placeholder placeholder-lg col-12 radius-8 btn-skeleton bg-secondary"></div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.card-goal-wrapper .card-header {
    margin-bottom: -1.5rem;
    padding: 1rem 1rem 1.5rem 1rem;
}

.card-goal-wrapper .card-footer {
    margin-top: -1.5rem;
}

.card-goal-wrapper .card-header,
.card-goal-wrapper .card-footer {
    z-index: 99;
    border: none;
}

.card-goal-wrapper .card-img {
    background-position: center !important;
    background-size: cover !important;
    background-repeat: no-repeat !important;
    display: flex;
    justify-content: center;
    align-items: start;
    height: 1000px;
    max-height: 480px;
}

.card-goal-wrapper.card {
    border: none;
}

.card-goal-wrapper .card-goal-title {
    height: 48px;
    line-height: 1.5rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
}

.card-goal-info .goal-reward .text-reward {
    line-height: 1.5rem;
}

.card-goal-wrapper .goal-label {
    height: auto;
    line-height: 1rem;
    display: block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
}

.card-goal-wrapper .goal-description {
    max-width: 75%;
    height: 4rem;
    display: block;
    overflow: hidden;
    text-overflow: ellipsis;
}

.card-goal-wrapper .card-body .body-content {
    max-width: 366px;
    width: 100%;
}

.card-footer {
    height: 4.5rem;
}
.skeleton .card-footer .btn-skeleton {
    height: 3.5rem;
}
.skeleton .card-body .img-skeleton {
    height: 500px;
}
</style>