<template>
  <p>
    {{scramble}}</p>
</template>

<script>
import Scrambler from './tnoodle'
//const tnoodle = require('./tnoodle').tnoodle

export default {
  data() {
    return {
      scramble: 'wating'
    }
  },
  methods: {
    next() {
      this.scrambler.loadScramble(res => {
        console.log(res)
        this.scramble = res
      }, '333')
    }
  },
  beforeCreate() {
    this.scrambler = new Scrambler()
  }
}
</script>

<style lang="scss">
p {
  color: yellow;
}
</style>
