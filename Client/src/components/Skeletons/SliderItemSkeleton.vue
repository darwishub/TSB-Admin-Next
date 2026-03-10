<template>
    <div class="skeleton">
        <div class="card-goal-wrapper card shadow--sm radius-16">
            <div class="card-header bg-white radius-16 shadow--md">
                <p class="placeholder-wave">
                    <span class="placeholder placeholder-lg col-12 radius-8 bg-secondary"></span>
                </p>
                <div class="card-goal-title text-sm-bold f-game ls-1 text-center">
                    <p class="placeholder-wave">
                        <span class="placeholder placeholder-lg col-6 radius-8 bg-secondary"></span>
                    </p>
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
.skeleton .card-goal-wrapper {
    width: 272px;
}

.skeleton .card-goal-wrapper.card {
    border: none;
}

.skeleton .card-header {
    height: 104px;
}
.skeleton .card-body .img-skeleton {
    height: 160px;
}
.skeleton .card-footer .btn-skeleton {
    height: 50px;
}
.card-goal-wrapper .card-header {
    margin-bottom: -1rem;
    z-index: 99;
}
.card-goal-wrapper .card-footer {
    margin-top: -1rem;
    z-index: 99;
} 
</style>