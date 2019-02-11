#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <stdint.h>
#include <errno.h>
#include <math.h>

#include <jpeglib.h>

#include "papi.h"

static int sobel_x_filter[3][3]={{-1,0,+1},{-2,0,+2},{-1,0,+1}};
static int sobel_y_filter[3][3]={{-1,-2,-1},{0,0,0},{1,2,+1}};

struct image_t {
	int x;
	int y;
	int depth;	/* bytes */
	unsigned char *pixels;
};


static void generic_convolve(struct image_t *input_image,
				struct image_t *output_image,
				int filter[3][3]) {


	/* Look at the above image_t definition			*/
	/* You can use input_image->x (width)  			*/
	/*             input_image->y (height) 			*/
	/*         and input_image->depth (bytes per pixel)	*/
	/* input_image->pixels points to the RGB values		*/

	/******************/
	/* Your code here */
	/******************/

	static int sum = 0;

	for(int y = 1; y < input_image->y - 1; y++)
	{
		for(int x = 1; x < input_image->x - 1; x++)
		{
			for(int color = 0; color < input_image->depth; color++)
			{
				sum = 0;
				
				sum += filter[0][0] * input_image->pixels[(y - 1) * input_image->x * input_image->depth + (x - 1) * input_image->depth + color];
				sum += filter[1][0] * input_image->pixels[(y - 1) * input_image->x * input_image->depth + x * input_image->depth + color];
				sum += filter[2][0] * input_image->pixels[(y - 1) * input_image->x * input_image->depth + (x + 1) * input_image->depth + color];
				sum += filter[0][1] * input_image->pixels[y * input_image->x * input_image->depth + (x - 1) * input_image->depth + color];
				sum += filter[1][1] * input_image->pixels[y * input_image->x * input_image->depth + x * input_image->depth + color];
				sum += filter[2][1] * input_image->pixels[y * input_image->x * input_image->depth + (x + 1) * input_image->depth + color];
				sum += filter[0][2] * input_image->pixels[(y + 1) * input_image->x * input_image->depth + (x - 1) * input_image->depth + color];
				sum += filter[1][2] * input_image->pixels[(y + 1) * input_image->x * input_image->depth + x * input_image->depth + color];
				sum += filter[2][2] * input_image->pixels[(y + 1) * input_image->x * input_image->depth + (x + 1) * input_image->depth + color];

				if(sum > 255)
				{
					sum = 255;
				}	
				if(sum < 0)
				{
					sum = 0;
				}
				
				output_image->pixels[y * input_image->x * input_image->depth + x * input_image->depth + color] = sum;
			}
		} 
	}
}


static int load_jpeg(char *filename, struct image_t *image) {

	FILE *fff;
	struct jpeg_decompress_struct cinfo;
	struct jpeg_error_mgr jerr;
	JSAMPROW output_data;
	unsigned int scanline_len;
	int scanline_count=0;

	fff=fopen(filename,"rb");
	if (fff==NULL) {
		fprintf(stderr, "Could not load %s: %s\n",
			filename, strerror(errno));
		return -1;
	}

	/* set up jpeg error routines */
	cinfo.err = jpeg_std_error(&jerr);

	/* Initialize cinfo */
	jpeg_create_decompress(&cinfo);

	/* Set input file */
	jpeg_stdio_src(&cinfo, fff);

	/* read header */
	jpeg_read_header(&cinfo, TRUE);

	/* Start decompressor */
	jpeg_start_decompress(&cinfo);

	printf("output_width=%d, output_height=%d, output_components=%d\n",
		cinfo.output_width,
		cinfo.output_height,
		cinfo.output_components);

	image->x=cinfo.output_width;
	image->y=cinfo.output_height;
	image->depth=cinfo.output_components;

	scanline_len = cinfo.output_width * cinfo.output_components;
	image->pixels=malloc(cinfo.output_width * cinfo.output_height * cinfo.output_components);

	while (scanline_count < cinfo.output_height) {
		output_data = (image->pixels + (scanline_count * scanline_len));
		jpeg_read_scanlines(&cinfo, &output_data, 1);
		scanline_count++;
	}

	/* Finish decompressing */
	jpeg_finish_decompress(&cinfo);

	jpeg_destroy_decompress(&cinfo);

	fclose(fff);

	return 0;
}

static int store_jpeg(char *filename, struct image_t *image) {

	struct jpeg_compress_struct cinfo;
	struct jpeg_error_mgr jerr;
	int quality=90; /* % */
	int i;

	FILE *fff;

	JSAMPROW row_pointer[1];
	int row_stride;

	/* setup error handler */
	cinfo.err = jpeg_std_error(&jerr);

	/* initialize jpeg compression object */
	jpeg_create_compress(&cinfo);

	/* Open file */
	fff = fopen(filename, "wb");
	if (fff==NULL) {
		fprintf(stderr, "can't open %s: %s\n",
			filename,strerror(errno));
		return -1;
	}

	jpeg_stdio_dest(&cinfo, fff);

	/* Set compression parameters */
	cinfo.image_width = image->x;
	cinfo.image_height = image->y;
	cinfo.input_components = image->depth;
	cinfo.in_color_space = JCS_RGB;
	jpeg_set_defaults(&cinfo);
	jpeg_set_quality(&cinfo, quality, TRUE);

	/* start compressing */
	jpeg_start_compress(&cinfo, TRUE);

	row_stride=image->x*image->depth;

	for(i=0;i<image->y;i++) {
		row_pointer[0] = & image->pixels[i * row_stride];
		jpeg_write_scanlines(&cinfo, row_pointer, 1);
	}

	/* finish compressing */
	jpeg_finish_compress(&cinfo);

	/* close file */
	fclose(fff);

	/* clean up */
	jpeg_destroy_compress(&cinfo);

	return 0;
}

static int combine(struct image_t *sobel_x, struct image_t *sobel_y,
		struct image_t *new_image) {

	/******************/
	/* your code here */
	/******************/
	for(int y = 0; y < new_image->y; y++)
	{
		for(int x = 0; x < new_image->x; x++)
		{
			new_image->pixels[y * new_image->x * new_image->depth + x * new_image->depth] = sqrt(pow(sobel_x->pixels[y * sobel_x->x * sobel_x->depth + x * sobel_x->depth], 2) + pow(sobel_y->pixels[y * sobel_y->x * sobel_y->depth + x * sobel_y->depth], 2));
		}
	}

	return 0;
}

int main(int argc, char **argv) {

	struct image_t image,sobel_x,sobel_y,new_image;
	int result;

	/* Check command line usage */
	if (argc<2) {
		fprintf(stderr,"Usage: %s image_file\n",argv[0]);
		return -1;
	}

	result=PAPI_library_init(PAPI_VER_CURRENT);
	if (result!=PAPI_VER_CURRENT) {
		fprintf(stderr,"Warning!  PAPI error %s\n",
			PAPI_strerror(result));
	}

	/* Load an image */
	load_jpeg(argv[1],&image);

	/* Allocate space for output image */
	new_image.x=image.x;
	new_image.y=image.y;
	new_image.depth=image.depth;
	new_image.pixels=malloc(image.x*image.y*image.depth*sizeof(char));

	/* Allocate space for output image */
	sobel_x.x=image.x;
	sobel_x.y=image.y;
	sobel_x.depth=image.depth;
	sobel_x.pixels=malloc(image.x*image.y*image.depth*sizeof(char));

	/* Allocate space for output image */
	sobel_y.x=image.x;
	sobel_y.y=image.y;
	sobel_y.depth=image.depth;
	sobel_y.pixels=malloc(image.x*image.y*image.depth*sizeof(char));

	/* convolution */
	generic_convolve(&image,&sobel_x,
			sobel_x_filter);
	generic_convolve(&image,&sobel_y,
			sobel_y_filter);
	combine(&sobel_x,&sobel_y,&new_image);

	store_jpeg("out.jpg",&new_image);

	PAPI_shutdown();

	return 0;
}
