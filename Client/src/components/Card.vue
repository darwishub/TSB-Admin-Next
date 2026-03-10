<template>
  <div class="card-option">
    <slot/>
  </div>
</template>

<script setup>
</script>

<style scoped>
.card-option.green-1 {
    background: rgba(56, 171, 109, 0.25);
    border: solid 2px #077E3E;
    border-radius: 0.5rem;
    color: #077E3E;
}

.card-option.gray-0 {
    border: solid 2px #C2C2C2;
    border-radius: 0.5rem;
    color: #C2C2C2;
    background: #fff;
}
/*== Main Green*/
.card-option.green {
    background: rgba(15, 147, 76, 1);
}
.card-option.green.o-75 {
    background: rgba(15, 147, 76, 0.75);
}

/*== Main Yellow*/
.card-option.yellow {
  background: rgba(223, 174, 85, 1);
}
.card-option.yellow.o-75 {
  background: rgba(223, 174, 85, 0.75);
}

</style>