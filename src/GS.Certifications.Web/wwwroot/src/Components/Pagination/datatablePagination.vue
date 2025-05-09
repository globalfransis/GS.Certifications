<template>
    <div class="dataTables_paginate paging_simple_numbers" :id="`${idPagination}_paginate`">
        <ul class="pagination custom-pagination">
          <li @click="goToPage(currentPageIndex - 1)" :disabled="isFirstPage"
            :class="isFirstPage ? 'paginate_button page-item previous disabled' : 'paginate_button page-item previous'"
            :id="`${idPagination}_previous`">
            <a :aria-controls="idPagination" tabindex="0" class="page-link">{{ loc['Anterior'] }}</a>
          </li>
          <template v-for="pageIndex, index in visiblePages">
            <li @click="goToPage(pageIndex)" :key="`${idPagination}-${pageIndex+currentPageIndex+index}`"
              :class="pageIndex === currentPageIndex ? 'paginate_button page-item active' : 'paginate_button page-item'">
              <a v-if="isPageIndex(pageIndex)" :aria-controls="idPagination" tabindex="0" class="page-link">
                {{ pageIndex + 1 }}
              </a>
              <a disabled v-else :aria-controls="idPagination" tabindex="0" class="page-link">
                {{ ellipsis }}
              </a>
            </li>
          </template>
          <li @click="goToPage(currentPageIndex + 1)" :disabled="isLastPage"
            :class="isLastPage ? 'paginate_button page-item next disabled' : 'paginate_button page-item next'"
            :id="`${idPagination}_next`">
            <a :aria-controls="idPagination" tabindex="0" class="page-link">{{ loc['Siguiente'] }}</a>
          </li>
        </ul>
      </div>
</template>

<script>
import loc from "@/common/commonLoc";


export default {
  name: "datatablePagination",

  components: {
    loc
  },
  
  props: {
    id: null,
    value: String,
    recordsTotal: Number,
    recordsLength: Number,
    maxVisiblePages: Number,
  },

  data: function () {
    return {
      loc,
      idPagination: `${this.id}-pagination`,
      ellipsis: '...'
    };
  },

  computed: {
    totalPages() {
      return Math.ceil(this.recordsTotal / this.recordsLength)
    },
    visiblePages() {
      let currentPageIndex = this.currentPageIndex;
      let totalPages = this.totalPages;
      let visiblePages = [];

      let maxVisiblePages = this.maxVisiblePages;

      if (totalPages <= maxVisiblePages) {
        // Si hay menos de 10 páginas, mostramos todas
        for (let i = 0; i < totalPages; i++) {
          visiblePages.push(i);
        }
      } else if (currentPageIndex < maxVisiblePages - 2) {
        // Si estamos en las primeras páginas
        for (let i = 0; i < maxVisiblePages; i++) {
          visiblePages.push(i);
        }
        visiblePages.push(this.ellipsis);
        visiblePages.push(totalPages - 1);
      } else if (currentPageIndex >= totalPages - (maxVisiblePages - 2)) {
        // Si estamos en las últimas páginas
        visiblePages.push(0);
        visiblePages.push(this.ellipsis);
        for (let i = totalPages - maxVisiblePages; i < totalPages; i++) {
          visiblePages.push(i);
        }
      } else {
        // Si estamos en páginas intermedias
        visiblePages.push(0);
        visiblePages.push(this.ellipsis);
        for (let i = currentPageIndex - 1; i <= currentPageIndex + 1; i++) {
          visiblePages.push(i);
        }
        visiblePages.push(this.ellipsis);
        visiblePages.push(totalPages - 1);
      }

      return visiblePages;
    },
    isFirstPage() {
      return this.currentPageIndex === 0;
    },
    isLastPage() {
      return this.currentPageIndex === this.totalPages - 1;
    },
    currentPageIndex: {
      get() {
        return this.value ? parseInt(this.value) : 0;
      },
      set(newPageIndex) {
        this.$emit('input', newPageIndex ? newPageIndex.toString() : '0')
        this.$emit('page-changed')
      },
    }
  },
  
  async mounted() {
  },

  methods: {
    isPageIndex(page) {
      return typeof page === 'number';
    },
    goToPage(pageIndex) {
        if (pageIndex >= 0 && pageIndex < this.totalPages && this.isPageIndex(pageIndex)) {
          this.currentPageIndex = pageIndex
        }
    }
  }
};
</script>

<style scoped>
.custom-pagination {
  display: flex;
  justify-content: right;
}
</style>