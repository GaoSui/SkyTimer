<template>
  <p>
    {{scramble}}</p>
</template>

<script>
import Scrambler from './tnoodle'

export default {
  data() {
    return {
      scramble: 'wating'
    }
  },
  methods: {
    next() {
      if (!this.scrambler) this.scrambler = new Scrambler()
      this.scrambler.loadScramble(res => this.scramble = res, '333')
    }
  }
}
</script>

